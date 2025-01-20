using AutoMapper;
using E_commerce.core;
using E_commerce.core.Dtos.Proudects;
using E_commerce.core.Models;
using E_commerce.core.Services.Interfaces;
using E_commerce.core.Specifiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Services.Proudects
{
    public class ProudectServicers : IProdectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProudectServicers(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<Brand, int>().GetAllAsync());
        }

        public async Task<IEnumerable<ProudectDtos>> GetAllProudectAsync(ProductSpecPrams productSpec)
        {
            var spec = new ProdectSpecifiction(productSpec);
            return _mapper.Map<IEnumerable<ProudectDtos>>(await _unitOfWork.Repository<Proudect, int>().GetAllWithSpecAsync(spec));
        
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<Types, int>().GetAllAsync());

        }

        public async Task<ProudectDtos> GetProudectByIdAsync(int id)
        {
            var spec=new ProdectSpecifiction(id);
            return _mapper.Map <ProudectDtos>(await _unitOfWork.Repository<Proudect, int>().GetWithSpecAsync(spec));
        }
    }
}
