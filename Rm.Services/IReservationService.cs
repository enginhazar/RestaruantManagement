using Rm.Core.Dtos;
using Rm.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Services
{
    public interface IReservationService
    {
        ServiceResponse<List<TableDto>> GetavailabilityTables(DateTime date, int size);

        ServiceResponse<ReservationDto> CreateReservation(ReservationDto reservationDto);

        ServiceResponse<bool> CancelReservation(Guid reservationId);

        ServiceResponse<List<ReservationDto>> GetAll();

    }
}
