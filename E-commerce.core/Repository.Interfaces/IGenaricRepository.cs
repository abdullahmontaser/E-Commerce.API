using E_commerce.core.Models;
using E_commerce.core.Specifiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Repository.Interfaces
{
    public interface IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
       Task<IEnumerable<TEntity>> GetAllAsync();
       Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifictin<TEntity, TKey> Spec);
       Task<TEntity> GetAsync(TKey id);
       Task<TEntity> GetWithSpecAsync(ISpecifictin<TEntity, TKey> Spec);
       Task AddAsync(TEntity entity);
       void Update(TEntity entity);
       void Delete(TEntity entity);
    }
}
