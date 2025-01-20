using E_commerce.core;
using E_commerce.core.Models;
using E_commerce.core.Repository.Interfaces;
using E_Commerce.Repository.Data.Contexts;
using E_Commerce.Repository.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repository;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _repository=new Hashtable();
        }
        public async Task<int> ComplyeteAsync()
        {
           return  await _context.SaveChangesAsync();        }
 
        public IGenaricRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type=typeof(TEntity).Name;
            if (!_repository.ContainsKey(type)) { 
            
            var repository= new GenaricRepoistory<TEntity, TKey>(_context);
                _repository.Add(type, repository); 
            }
            return _repository[type]as IGenaricRepository<TEntity, TKey>;
        }
    }
}
