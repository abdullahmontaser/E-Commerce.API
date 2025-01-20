using AutoMapper;
using E_commerce.core.Dtos.Auth;
using E_commerce.core.Models.Identity;
using E_commerce.core.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;


        public UserService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenServices tokenServices,
            IMapper mapper
            
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }

      

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user=await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return null;
           var result = await  _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if (!result.Succeeded) return null;
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token =  await _tokenServices.CreateTokenAsync(user,_userManager)
            };
        }

        public async Task<UserDto> RigisterAsync(RigisterDto rigisterDto)
        {

            if (await CheakEmailExitsAsync(rigisterDto.Email)) return null;
            var user = new AppUser()
            {
                Email = rigisterDto.Email,
                DisplayName = rigisterDto.DisplayName,
                PhoneNumber = rigisterDto.PhoneNumber,
                UserName = rigisterDto.Email.Split("@")[0]
                
            };
           var result= await _userManager.CreateAsync(user,rigisterDto.Password);
            if (!result.Succeeded) return null;
            return new UserDto()
            {
                Email=user.Email,
                DisplayName=user.DisplayName,
                Token = await _tokenServices.CreateTokenAsync(user, _userManager)
            };
        }
        public async Task<bool> CheakEmailExitsAsync(string email)
        {
                 
            return await _userManager.FindByEmailAsync(email) is null;
        }

        public async Task<AppUser> UpdateAddress(AddressDto addressDto, ClaimsPrincipal User)
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            if (UserEmail is null) return null;
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(U => U.Email == UserEmail);
            var mappaddress = _mapper.Map<Address>(addressDto);
            mappaddress.Id = user.Address.Id;
            user.Address = mappaddress;
            return user;
        }
    }
}
