using AutoMapper;
using E_commerce.core.Dtos;
using E_commerce.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Mapping
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<UserBasket,UserBasketDto>().ReverseMap();
        }
    }
}
