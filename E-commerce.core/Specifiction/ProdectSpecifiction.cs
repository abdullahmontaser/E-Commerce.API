using E_commerce.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Specifiction
{
    public class ProdectSpecifiction :BaseSpecifictin<Proudect,int>
    {
        public ProdectSpecifiction(int id) : base(P=>P.Id==id) 
        {
            includes();
        }
        public ProdectSpecifiction(ProductSpecPrams productSpec) : base(
            P=>
            (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search))
            &&
            (!productSpec.BrandId.HasValue || productSpec.BrandId== P.BrandId)
            &&
            (!productSpec.TaypId.HasValue || productSpec.TaypId == P.TypeId)
            )
        {
          
            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort) 
                {
                   
                         case "priceAsc":
                        OrderBy = P => P.Price;
                        break; case "priceDec":
                        OrderByDec = P => P.Price;
                        break;
                        default:       
                        OrderBy = P => P.Name;
                        break;
                }

            }
            else 
            { 
            OrderBy = P=>P.Name;
            }
            includes();
            //900
            //page size 50
            //page index 3
            ApplyPagintion(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);
        }
        private  void includes()
        {
            Inclouds.Add(P => P.Brand);
            Inclouds.Add(P => P.Type);
        }
    }
}
