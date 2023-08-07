using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using Rm.Core.Dtos;
using Rm.Core.Mapping;
using Rm.Services;
using Rm.Test.Mock;
using Rm.Test.Mocks;
using Xunit;

namespace Rm.Test
{
    public class ReservationServiceTest
    {
        private readonly IMapper _mapper;
        private readonly MockReservationTableRepository _reservationTableRepository;
        private readonly MockReservationRepository _reservationRepository;
        private readonly MockTableRepository _tableRepository;
        private readonly Mock<ILogger<ReservationService>> _logger;
        public ReservationServiceTest()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
                                               cfg =>
                                               {
                                                   cfg.AddProfile(new MappingConfiguration());
                                               });

            _mapper = new Mapper(mapperConfig);
            _reservationTableRepository = new MockReservationTableRepository();
            _reservationRepository = new MockReservationRepository();
            _tableRepository = new MockTableRepository();
            _logger = new Mock<ILogger<ReservationService>>();

        }

        [Fact]
        public void GetAviableTables_Should()
        {
            var reservationService = new ReservationService(_logger.Object, _mapper, _reservationRepository, _reservationTableRepository, _tableRepository);
            ServiceResponse<List<TableDto>> serviceResponse = reservationService.GetavailabilityTables(new DateTime(2023, 8, 7), 5);

            Xunit.Assert.True(serviceResponse.Success);

        }

        [Fact]
        public void GetAviableTables_Desired_Capacity_Not_Should()
        {


            var reservationService = new ReservationService(_logger.Object, _mapper, _reservationRepository, _reservationTableRepository, _tableRepository);
            ServiceResponse<List<TableDto>> serviceResponse = reservationService.GetavailabilityTables(new DateTime(2023, 8, 6), 9);

            Xunit.Assert.False(serviceResponse.Success);

        }

        [Fact]
        public void CreateReservation_Should_Correct()
        {
            ReservationDto reservationDto = CreateReservationDto();

            var reservationService = new ReservationService(_logger.Object, _mapper, _reservationRepository, _reservationTableRepository, _tableRepository);
            ServiceResponse<ReservationDto> serviceResponse = reservationService.CreateReservation(reservationDto);
            Xunit.Assert.True(serviceResponse.Success);
        }


        [Fact]
        public void CreateReservation_Zero_Capacity_Should_Correct()
        {
            ReservationDto reservationDto = CreateReservationDto();
            reservationDto.Size=0;

            var reservationService = new ReservationService(_logger.Object, _mapper, _reservationRepository, _reservationTableRepository, _tableRepository);
            ServiceResponse<ReservationDto> serviceResponse = reservationService.CreateReservation(reservationDto);
            Xunit.Assert.False(serviceResponse.Success);
        }

        [Fact]
        public void CreateReservation_CheckCustomer_Capacity_Should_Correct()
        {
            ReservationDto reservationDto = CreateReservationDto();
            reservationDto.Size=9;

             var reservationService = new ReservationService(_logger.Object, _mapper, _reservationRepository, _reservationTableRepository, _tableRepository);
            ServiceResponse<ReservationDto> serviceResponse = reservationService.CreateReservation(reservationDto);
            Xunit.Assert.False(serviceResponse.Success);
        }

        [Fact]
        public void CancelReservation_Should_Correct()
        {

            Guid reservationGuid = new Guid("742a28d5-1794-46ee-9d70-a8999fc482bb");

            var reservationService = new ReservationService(_logger.Object, _mapper, _reservationRepository, _reservationTableRepository, _tableRepository);
            ServiceResponse<bool> serviceResponse = reservationService.CancelReservation(reservationGuid);
            Xunit.Assert.True(serviceResponse.Success);
        }
        private static ReservationDto CreateReservationDto()
        {
            return new ReservationDto()
            {
                CustomerName ="Engin",
                CustomerSurname="Hazar",
                MobilePhone="13467946",
                ReservationDateTime =new DateTime(2023, 8, 6),
                Size=4
            };
        }


    }
}