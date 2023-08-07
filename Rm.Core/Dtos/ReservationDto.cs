using Rm.Core.Entity;
using System;

namespace Rm.Core.Dtos
{
    public class ReservationDto : BaseDto
    {
        public string CustomerName { get; set; }

        public string CustomerSurname { get; set; }

        public string MobilePhone { get; set; }
        public DateTime ReservationDateTime { get; set; }
        public int Size { get; set; }
    }
}
