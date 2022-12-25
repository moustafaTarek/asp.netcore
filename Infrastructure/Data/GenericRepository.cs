using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;
        private readonly DbSet<T> _Entity;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _Entity = _storeContext.Set<T>();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _Entity.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _Entity.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync(); 
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return  SpecificationEvaluator<T>.GetQuery(_Entity.AsQueryable(), spec);
        }
    }
}