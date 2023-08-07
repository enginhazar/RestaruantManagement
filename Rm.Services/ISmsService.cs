using Rm.Core.Dtos;

namespace Rm.Services
{
    public interface ISmsService
    {
        
        ServiceResponse<bool> SendSms(ReservationDto reservationDto);
    }
}