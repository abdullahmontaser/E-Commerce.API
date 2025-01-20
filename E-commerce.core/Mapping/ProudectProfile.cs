using AutoMapper;
using E_commerce.core.Dtos.Auth;
using E_commerce.core.Dtos.Order;
using E_commerce.core.Dtos.Proudects;
using E_commerce.core.Models;
using E_commerce.core.Models.Order;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Mapping
{
    public class ProudectProfile : Profile
    {
        public ProudectProfile(IConfiguration configuration) {
            CreateMap<Proudect, ProudectDtos>().
                   ForMember(d => d.BrandName, options => options.MapFrom(s => s.Brand.Name))
                  .ForMember(d => d.TypeName, options => options.MapFrom(s => s.Type.Name))
                  .ForMember(d => d.PictureUrl, option => option.MapFrom(s => $"{configuration["BaseUrl"]}{s.PictureUrl}"));

         CreateMap<Brand,TypeBrandDto>();
         CreateMap<Types,TypeBrandDto>();
         CreateMap<ProudectDtos,Proudect>();
         CreateMap<AddressDto,Address>().ReverseMap();
         CreateMap<Order, OrderToReturnDto>().
                   ForMember(d => d.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName)).
                   ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                   .ForMember(d => d.ProudectId, o => o.MapFrom(S => S.Proudect.ProudectId))
                   .ForMember(d => d.ProudectName, o => o.MapFrom(S => S.Proudect.ProudectName))
                   .ForMember(d => d.PictureUrl, o => o.MapFrom(S => S.Proudect.PictureUrl))
                   .ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"{configuration["BaseUrl"]}{s.Proudect.PictureUrl}"));
        
                   
        }
        
    }
}
