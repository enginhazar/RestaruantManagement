using Rm.Core.Entity;
using Rm.Core.Interfaces;
using Rm.Test.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Test.Mocks
{
    internal class MockReservationRepository : IRepository<Reservation>
    {
        private readonly List<Reservation> _reservationData;

        public MockReservationRepository()
        {
            _reservationData = new List<Reservation>();
            _reservationData.Add(
                new Reservation()
                {
                    Id = new Guid("742a28d5-1794-46ee-9d70-a8999fc482bb"),
                    ReservationDateTime = DateTime.Now,
                });

        }
        public void Add(Reservation entity)
        {
            _reservationData.Add(entity);
        }

        public void Delete(Guid id)
        {
            Reservation reservation = _reservationData.Where(x=> x.Id == id).FirstOrDefault<Reservation>();
            _reservationData.Remove(reservation);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Reservation Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Reservation> Include(Func<IQueryable<Reservation>, IQueryable<Reservation>> include)
        {
            throw new NotImplementedException();
        }

        public void Update(Reservation entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Reservation> Where(Expression<Func<Reservation, bool>> predicate)
        {
            return _reservationData.Where(predicate.Compile());
        }
    }
}
