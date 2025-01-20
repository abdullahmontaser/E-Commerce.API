using E_commerce.core.Dtos.Proudects;
using E_commerce.core.Models;
using E_commerce.core.Specifiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Services.Interfaces
{
    public interface IProdectService
    {
        Task<IEnumerable<ProudectDtos>> GetAllProudectAsync(ProductSpecPrams productSpec);
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<ProudectDtos> GetProudectByIdAsync(int id);
         
    }
}
