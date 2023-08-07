using Rm.Core.Entity;
using Rm.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Test.Mock
{
    internal class MockReservationTableRepository : IRepository<ReservationTable>
    {
        private readonly List<ReservationTable> _reservationsData;


        public MockReservationTableRepository()
        {
            _reservationsData = new List<ReservationTable>();
            GenerateData();
        }
        public void GenerateData()
        {
            Reservation reservation = new Reservation();
            reservation.ReservationDateTime = new DateTime(2023, 08, 6);
 
            //    _reservationsData = new List<ReservationTable>();

            _reservationsData.Add(
                new ReservationTable()
                {
                    Reservation = reservation,
                    ReserveTable = new Table() { TableName = "M1", Capacity = 5, Id = new Guid("742a28d5-1794-46ee-9d70-a8999fc482bb") }
                });
            _reservationsData.Add(
                new ReservationTable()
                {
                    Reservation = reservation,
                    ReserveTable = new Table() { TableName = "M2", Capacity = 3, Id = new Guid("84b29d0d-903b-4758-b495-138cca40c6dd") }
                });
        }

        public void Add(ReservationTable entity)
        {
            _reservationsData.Add(entity);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ReservationTable Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationTable> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ReservationTable entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationTable> Where(Expression<Func<ReservationTable, bool>> predicate)
        {
            return _reservationsData.Where(predicate.Compile());
        }

        public IQueryable<ReservationTable> Include(Func<IQueryable<ReservationTable>, IQueryable<ReservationTable>> include)
        {
            throw new NotImplementedException();
        }
    }
}
