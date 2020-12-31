using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Backend.Domain.Contract
{
    public interface IDomainBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            params Expression<Func<T, object>>[] includes);

        Task<T> Find(Guid id);

        Task<int> Create(T entity);

        Task<bool> Update(T entity);

        Task<int> Delete(T entity);
    }
}