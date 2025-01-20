using E_commerce.core.Dtos.Auth;
using E_commerce.core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RigisterAsync(RigisterDto rigisterDto);
        Task<AppUser> UpdateAddress(AddressDto addressDto, ClaimsPrincipal User);
        Task<bool> CheakEmailExitsAsync(string email);
    }
}
