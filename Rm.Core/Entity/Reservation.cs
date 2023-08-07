using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rm.Core.Entity
{
    public class Reservation : EntityBase
    {
        public string CustomerName { get; set; }

        public string CustomerSurname { get; set; }

        public string MobilePhone { get; set; }

        public DateTime ReservationDateTime { get; set; }
        public int Size { get; set; }

        public virtual List<ReservationTable> ReservationTables { get; set; }
    }
}
