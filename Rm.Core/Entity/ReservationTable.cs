using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Core.Entity
{
    public class ReservationTable : EntityBase
    {
        public  virtual Reservation Reservation { get; set; }
        public  virtual Table ReserveTable { get; set; }
    }
}
