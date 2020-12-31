﻿using Microsoft.EntityFrameworkCore;
using Restaurant.Backend.Entities.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Restaurant.Backend.Entities.Entities;

namespace Restaurant.Backend.Repositories.Infrastructure
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger _logger;

        private bool _disposed;

        public GenericRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<T>();
        }

        public Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return Task.FromResult(query);
        }

        public async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task<IQueryable<T>> GetBy(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult(_dbSet.Where(predicate));
        }

        public async Task<int> Create(T entity)
        {
            try
            {
                entity.CreationDate = DateTimeOffset.UtcNow;
                entity.ModificationDate = DateTimeOffset.MinValue;
                
                await _dbSet.AddAsync(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message, ex);
                return 0;
            }
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                entity.ModificationDate = DateTimeOffset.UtcNow;

                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message, ex);
                return false;
            }
        }

        public async Task<int> Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message, ex);
                return 0;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
