using Rm.Core.Dtos;
using Rm.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Services
{
    public class SmsService : ISmsService
    {
        private readonly ISmsProvider _smsProvider;
        public SmsService(ISmsProvider smsProvider)
        {
            _smsProvider =smsProvider;
        }
        public ServiceResponse<bool> SendSms(ReservationDto reservationDto)
        {
            ServiceResponse<bool> returnResponse = new ServiceResponse<bool>() { Success = false };



            if (string.IsNullOrEmpty(reservationDto.MobilePhone))
            {
                returnResponse.Success = false;
                returnResponse.Error.ErrorMessage = $"Customer mobile phone can not be null";
                return returnResponse;
            }
            try
            {
                var smsContent = $"Sayın {reservationDto.CustomerName} {reservationDto.CustomerSurname} {reservationDto.ReservationDateTime} tarihinde {reservationDto.Size}  kişilk rezervasyonunuz alınmıştır";
                _smsProvider.SendSms(reservationDto.MobilePhone, smsContent);
            }
            catch (Exception)
            {

                throw;
            }

            returnResponse.Success = true;
            return returnResponse;


        }
    }
}
