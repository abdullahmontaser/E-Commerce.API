using E_commerce.core.Models;
using E_commerce.core.Repository.Interfaces;
using E_commerce.core.Specifiction;
using E_Commerce.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repository
{
    public class GenaricRepoistory<TEntity, TKey> : IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenaricRepoistory(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
             await _context.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == (typeof(Proudect))) {
              return (IEnumerable <TEntity>) await _context.Proudects.Include(p => p.Brand).Include(p => p.Type).ToListAsync();
            }
           return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifictin<TEntity, TKey> Spec)
        {
          return await ApllyQuery(Spec).ToListAsync();
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            if (typeof(TEntity) == (typeof(Proudect)))
            {
                return  await _context.Proudects.Include(p => p.Brand).Include(p => p.Type).FirstOrDefaultAsync(p => p.Id==id as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetWithSpecAsync(ISpecifictin<TEntity, TKey> Spec)
        {
            return await ApllyQuery(Spec).FirstOrDefaultAsync();
        }
        
        public void Update(TEntity entity)
        {
           _context.Update(entity);
        }

        private IQueryable<TEntity> ApllyQuery(ISpecifictin<TEntity, TKey> Spec)
        {
            return SpecifictinEvaluator<TEntity, TKey>.GetQuary(_context.Set<TEntity>(), Spec);
        }
    }
}
