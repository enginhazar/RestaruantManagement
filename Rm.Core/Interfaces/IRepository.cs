using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rm.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Add(T entity);
        void Delete(Guid id);
        void Update(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);

        IQueryable<T> Include(Func<IQueryable<T>, IQueryable<T>> include);
        T Get(Guid id);
    }
}
