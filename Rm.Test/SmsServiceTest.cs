using Moq;
using Rm.Core.Dtos;
using Rm.Core.Entity;
using Rm.Core.Interfaces;
using Rm.Services;
using Rm.Test.Mocks;
using Xunit;

namespace Rm.Test
{
    public class SmsServiceTest
    {
        private readonly ISmsProvider _smsProvider;
        public SmsServiceTest()
        {
            _smsProvider = new MockSmsProvider();
        }
        [Fact]
        public void SendSms_Should()
        {
            ReservationDto reservationDto = CreateReservation();
            var smsService = new SmsService(_smsProvider);
            ServiceResponse<bool> serviceResponse = smsService.SendSms(reservationDto);
            Assert.True(serviceResponse.Success);
        }

        [Fact]
        public void SendSms_Customer_PhoneNumber_Empty_BeShould()
        {
            ReservationDto reservationDto = CreateReservation();
            reservationDto.MobilePhone= "";
            var smsService = new SmsService(_smsProvider);
            ServiceResponse<bool> serviceResponse = smsService.SendSms(reservationDto);
            Assert.False(serviceResponse.Success);
        }

        private static ReservationDto CreateReservation()
        {
            return new ReservationDto()
            {
                ReservationDateTime =DateTime.Now,
                Size =3,
                CustomerName="Engin",
                CustomerSurname="Hazar",
                MobilePhone="5327555555"
            };
        }
    }
}
