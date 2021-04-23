using Authorization.Data;
using Authorization.Rpositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Authorization.Rpositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity,TKey> 
        where TEntity : class
        where TKey : struct
    {
        protected readonly AuthorizationDbContext _context;

        public Repository()
        {
        }

        public Repository(AuthorizationDbContext context)
        {
            _context = context;
        }

        public EntityEntry<TEntity> Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public TEntity GetById(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }
    }
}