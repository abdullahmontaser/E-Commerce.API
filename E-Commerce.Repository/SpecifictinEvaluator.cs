using E_commerce.core.Models;
using E_commerce.core.Specifiction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository
{
    public class SpecifictinEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuary(IQueryable<TEntity> inputQuery,ISpecifictin<TEntity,TKey> Spec)
        {
            var query=inputQuery;
            if(Spec.Criteria is not null)
            {
              query= query.Where(Spec.Criteria);
            }
            if (Spec.OrderBy is not null)
            {
                query = query.OrderBy(Spec.OrderBy);
            }  
            if (Spec.OrderByDec is not null)
            {
                query = query.OrderByDescending(Spec.OrderByDec);
            }
            if (Spec.IsPaginationEnabled)
            {
                query = query.Skip(Spec.Skip).Take(Spec.Take);
            }

            query = Spec.Inclouds.Aggregate(query, (courntquery, includeExprtion) => courntquery.Include(includeExprtion));


            return query;
        }
    }
}
