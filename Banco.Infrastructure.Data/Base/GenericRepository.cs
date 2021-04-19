using Banco.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Infrastructure.Data.Base
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected IDbContext _db;
        protected readonly DbSet<T> _dbset;
        protected GenericRepository(IDbContext context)
        {
            _db = context;
            _dbset = context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbset.Add(entity);
        }
        public void AddRange(List<T> entities)
        {
            _dbset.AddRange(entities);
        }
        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
        public void DeleteRange(List<T> entities)
        {
            _dbset.RemoveRange(entities);
        }
        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
        public T Find(object id)
        {
            return _dbset.Find(id);
        }
        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate).AsEnumerable();
        }
        public IEnumerable<T> FindBy(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public T FindFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbset.FirstOrDefault(predicate);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable();
        }
    }
}
