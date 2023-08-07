using Microsoft.EntityFrameworkCore;
using Rm.Core.Entity;
using Rm.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rm.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {

        private readonly RmDbContext _dbContext;
        private DbSet<T> _dbEntity;
        public Repository(RmDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbEntity = dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbEntity.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbEntity.Attach(entity);

            _dbEntity.Remove(entity);

            _dbContext.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var entity = Get(id);
            Delete(entity);
        }

        public IQueryable<T> Include(Func<IQueryable<T>, IQueryable<T>> include)
        {
            
            return include(_dbContext.Set<T>());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
        public T Get(Guid id)
        {
            var entity = _dbEntity.Find(id);

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbEntity.ToList();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbEntity.Where(predicate).ToList();
        }

        public void Update(T entity)
        {
            _dbEntity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            _dbContext.SaveChangesAsync();
        }
    }
}
