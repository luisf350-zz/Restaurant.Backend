using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Restaurant.Backend.Entities.Entities;

namespace Restaurant.Backend.Repositories.Infrastructure
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            params Expression<Func<T, object>>[] includes);

        Task<T> GetById(object id);

        Task<IQueryable<T>> GetBy(Expression<Func<T, bool>> predicate);

        Task<int> Create(T entity);

        Task<bool> Update(T entity);

        Task<int> Delete(T entity);
    }
}