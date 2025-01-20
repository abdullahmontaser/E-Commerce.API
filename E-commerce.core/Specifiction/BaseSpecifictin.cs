using E_commerce.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Specifiction
{
    public class BaseSpecifictin<TEntity, TKey> : ISpecifictin<TEntity, TKey>where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Inclouds { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderByDec { get; set; } = null;
        public int Skip { get ; set; }
        public int Take { get ; set ; }
        public bool IsPaginationEnabled { get; set ; }

        public BaseSpecifictin(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
        }
        public BaseSpecifictin()
        {
            
        }
        public void ApplyPagintion(int skip, int take) 
        { 
        IsPaginationEnabled = true;
            Skip= skip;
            Take= take;
        
        }
    }
    
}
