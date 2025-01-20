using AutoMapper;
using E_commerce.core.Dtos.Auth;
using E_commerce.core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Mapping
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
