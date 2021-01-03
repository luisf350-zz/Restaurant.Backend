using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Entities.Entities;
using Restaurant.Backend.Repositories.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Backend.Domain.Implementation
{
    public class DomainBase<T> : IDomainBase<T> where T : EntityBase
    {
        protected readonly IGenericRepository<T> Repository;

        public DomainBase(IGenericRepository<T> repository)
        {
            Repository = repository;
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            return await Repository.GetAll(filter, orderBy, includes);
        }

        public async Task<T> Find(Guid id)
        {
            return await Repository.GetById(id);
        }

        public async Task<T> FirstOfDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            return await Repository.FirstOfDefaultAsync(filter, includes);
        }

        public async Task<int> Create(T entity)
        {
            return await Repository.Create(entity);
        }

        public async Task<bool> Update(T entity)
        {
            return await Repository.Update(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await Repository.Delete(entity);
        }
    }
}