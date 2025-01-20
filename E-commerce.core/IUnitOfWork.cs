using E_commerce.core.Models;
using E_commerce.core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core
{
    public interface IUnitOfWork
    {
        Task<int> ComplyeteAsync();

        IGenaricRepository<TEntity,TKey> Repository<TEntity,TKey>() where TEntity :BaseEntity<TKey>;


    }
}
