using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Repositories.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Backend.Domain.Implementation
{
    public class DomainBase<T> : IDomainBase<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public DomainBase(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetAll(filter, orderBy, includes);
        }

        public async Task<T> Find(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<int> Create(T entity)
        {
            return await _repository.Create(entity);
        }

        public async Task<bool> Update(T entity)
        {
            return await _repository.Update(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await _repository.Delete(entity);
        }
    }
}