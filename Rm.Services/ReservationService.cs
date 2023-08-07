using AutoMapper;
using Microsoft.Extensions.Logging;
using Rm.Core.Dtos;
using Rm.Core.Entity;
using Rm.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ILogger<ReservationService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Reservation> _reservationRepository;
        internal readonly IRepository<ReservationTable> _reservationTableRepository;
        internal readonly IRepository<Table> _tableRepository;

        private object reservationlockObject = new object();

        public ReservationService(ILogger<ReservationService> logger,
            IMapper mapper,
            IRepository<Reservation> reservationRepository,
            IRepository<ReservationTable> reservationTableRepository,
            IRepository<Table> tableRepository
            )
        {
            _logger = logger;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _reservationTableRepository = reservationTableRepository;
            _tableRepository = tableRepository;

        }

        /// <summary>
        /// Rezervasyon işlemi
        /// </summary>
        /// <param name="reservationDate">Rezervasyon Tarihi</param>
        /// <param name="customerDto">Müşteri</param>
        /// <param name="desiredCapacity">İstenen Kapasite</param>
        /// <returns></returns>
        public ServiceResponse<ReservationDto> CreateReservation(ReservationDto reservationDto)
        {
            ServiceResponse<ReservationDto> returnResponse = new ServiceResponse<ReservationDto>() { Success = false };
            if (reservationDto.Size < 1)
            {
                returnResponse.Success = false;
                returnResponse.Error.ErrorMessage = "Size cannot be less than 1";
                return returnResponse;
            }

            var reservationCustomer = _reservationTableRepository.Where(x => x.Reservation.MobilePhone == reservationDto.MobilePhone
                    && x.Reservation.ReservationDateTime == reservationDto.ReservationDateTime)
                .Select(c => c.Reservation);

            if (reservationCustomer.Any())
            {
                returnResponse.Success = false;
                returnResponse.Error.ErrorMessage = $"{reservationDto.CustomerName} {reservationDto.CustomerSurname} has a reservation on {reservationDto.ReservationDateTime}";
                return returnResponse;
            }

            var aviableTables = GetAviableTables(reservationDto.ReservationDateTime);

            if (!IsAviableTable(reservationDto.Size, aviableTables))
            {
                returnResponse.Success = false;
                returnResponse.Error.ErrorMessage = $"There is no table to reserve on {reservationDto.ReservationDateTime}";
                return returnResponse;
            }

            Reservation reservation = new Reservation();

            // rezervasyon işlemi bitinceye kadar diğer rezervasyonlar işlemi bekletiliyor
            // bunun yerine distributed lock olabilir (redislock vb.)
            lock (reservationlockObject)
            {
                try
                {
                    reservation.Id = Guid.NewGuid();
                    reservation.CustomerName = reservationDto.CustomerName;
                    reservation.CustomerSurname = reservationDto.CustomerSurname;
                    reservation.MobilePhone = reservationDto.MobilePhone;
                    reservation.ReservationDateTime = reservationDto.ReservationDateTime;
                    reservation.CustomerName = reservationDto.CustomerName;
                    //reservation.CustomerName = customer;
                    reservation.ReservationTables=CreateReservtionTables(reservationDto.Size, aviableTables, reservation);
                    reservation.Size = reservationDto.Size;

                    _reservationRepository.Add(reservation);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "CreaateReservation");
                    returnResponse.Success = false;
                    returnResponse.Error.ErrorMessage = ex.ToString();
                    returnResponse.Error.Exception = ex;
                    return returnResponse;
                }

            }
            returnResponse.Success = true;
            returnResponse.Data = _mapper.Map<ReservationDto>(reservation);
            return returnResponse;

        }

        private void SendNotification(ReservationDto reservationDto)
        {
            /// Sms için provider elde et
           // SmsService
        }

        /// <summary>
        /// Rezervasyon İptail
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResponse<bool> CancelReservation(Guid reservationId)
        {
            ServiceResponse<bool> returnResponse = new ServiceResponse<bool>() { Success = false };
            try
            {

                _reservationRepository.Delete(reservationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CancelReservation");
                returnResponse.Success = false;
                returnResponse.Error.ErrorMessage = ex.ToString();
                returnResponse.Error.Exception = ex;
                return returnResponse;
            }
            returnResponse.Success = true;
            return returnResponse;
        }


        /// <summary>
        /// Uygun Masaların Listelenmesi
        /// </summary>
        /// <param name="reservationDate">Rezervasyon Tarihi</param>
        /// <param name="desiredCapacity">İstenen Kapasite</param>
        /// <returns></returns>
        public ServiceResponse<List<TableDto>> GetavailabilityTables(DateTime reservationDate, int desiredCapacity)
        {
            ServiceResponse<List<TableDto>> returnResponse = new ServiceResponse<List<TableDto>>() { Success = false };

            if (desiredCapacity < 1)
            {
                returnResponse.Success = false;
                returnResponse.Error.ErrorMessage = $"Size cannot be less than 1\"";
                return returnResponse;
            }

            IEnumerable<Table> aviableTables = GetAviableTables(reservationDate);

            if (!IsAviableTable(desiredCapacity, aviableTables))
            {
                returnResponse.Success = false;
                returnResponse.Error.ErrorMessage = $"There is no table to reserve on {reservationDate}";
                return returnResponse;
            }

            returnResponse.Success = true;
            returnResponse.Data = _mapper.Map<List<TableDto>>(aviableTables);
            return returnResponse;
        }

        public ServiceResponse<List<ReservationDto>> GetAll()
        {
            ServiceResponse<List<ReservationDto>> result = new ServiceResponse<List<ReservationDto>>();
            result.Success=true;

            var data = _reservationRepository.GetAll().ToList();
            result.Data = _mapper.Map<List<ReservationDto>>(data);
            return result;
        }


        /// <summary>
        /// Uygun Masaların getirilmesi
        /// </summary>
        /// <param name="reservationDate">Rezervasyon Tarihi</param>
        /// <param name="desiredCapacity">İstenen Kapasite</param>
        /// <returns></returns>
        private IEnumerable<Table> GetAviableTables(DateTime reservationDate)
        {
            var reservationTables = _reservationTableRepository
                .Where(x => x.Reservation.ReservationDateTime.Date == reservationDate.Date)
               .Select(x => x.ReserveTable.Id);

            if (reservationTables.Count()  == 0)
                return _tableRepository.GetAll();

            IEnumerable<Table> ret = _tableRepository.GetAll().Where(x => reservationTables.Contains(x.Id));
            return ret;
        }

        /// <summary>
        /// Rezervasyona Uygun masalar seçiliyor
        /// </summary>
        /// <param name="desiredCapacity">İstenen Kapasite</param>
        /// <param name="aviableTables"> Olabilecek Maslar</param>
        /// <param name="reservation">Rezervasyon bilgisi</param>
        /// <returns></returns>
        private static List<ReservationTable> CreateReservtionTables(int desiredCapacity, IEnumerable<Table> aviableTables, Reservation reservation)
        {
            var selectedtables = new List<ReservationTable>();
            var currentTotalCapacity = 0;
            foreach (var table in aviableTables)
            {
                currentTotalCapacity += table.Capacity;
                selectedtables.Add(new ReservationTable()
                {
                    Id = Guid.NewGuid(),
                    Reservation = reservation,
                    ReserveTable = table,
                });

                if (currentTotalCapacity >= desiredCapacity)
                    break;
            }

            return selectedtables;
        }

        private static bool IsAviableTable(int desiredCapacity, IEnumerable<Table> aviableTables)
        {
            return aviableTables == null || aviableTables.Count() == 0 || aviableTables.Sum(x => x.Capacity) >= desiredCapacity;
        }


    }
}
