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
    internal class MockTableRepository : IRepository<Table>
    {
        private readonly List<Table> _tableData;

        public MockTableRepository()
        {

            _tableData = new List<Table>();
            _tableData.Add(
                new Table()
                {
                    Id = new Guid("742a28d5-1794-46ee-9d70-a8999fc482bb"),
                    TableName = "M1",
                    Capacity = 5
                });
            _tableData.Add(
                new Table()
                {
                    Id = new Guid("84b29d0d-903b-4758-b495-138cca40c6dd"),
                    TableName = "M2",
                    Capacity = 3
                });
            _tableData.Add(
                new Table()
                {
                    Id = new Guid("f3805155-fffb-4fc5-8b9e-2859075c760d"),
                    TableName = "M3",
                    Capacity = 4
                });
        }
        public void Add(Table entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Table Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Table> GetAll()
        {
            return _tableData.ToList();
        }

        public IQueryable<Table> Include(Func<IQueryable<Table>, IQueryable<Table>> include)
        {
            throw new NotImplementedException();
        }

        public void Update(Table entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Table> Where(Expression<Func<Table, bool>> predicate)
        {
            return _tableData.Where(predicate.Compile());

        }
    }
}
