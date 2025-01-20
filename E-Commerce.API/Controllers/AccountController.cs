using AutoMapper;
using E_commerce.core.Dtos.Auth;
using E_commerce.core.Models.Identity;
using E_commerce.core.Services.Interfaces;
using E_Commerce.API.Erorrs;
using E_Commerce.API.Extention;
using E_Commerce.Services.Services.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using StackExchange.Redis;
using System.Net;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService,
            UserManager<AppUser> userManager,
            ITokenServices tokenServices,
            IMapper mapper
            )
        {
            _userService = userService;
            _userManager = userManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
           var user= await _userService.LoginAsync(loginDto);
            if (user is null) 
            
            return Unauthorized(new ApiErorrsResponse(StatusCodes.Status401Unauthorized));
            
            return Ok(user);
        }      
        [HttpPost("rigister")]
        public async Task<IActionResult> Rigister(RigisterDto rigisterDto)
        {
           var user= await _userService.RigisterAsync(rigisterDto);
            if (user is null) 
            
            return BadRequest(new ApiErorrsResponse(StatusCodes.Status400BadRequest, "Invalid_Rigisteriton"));
            
            return Ok(user);
        }
        [HttpGet("GetCurrntUser")] //Get:/api/Account/GetCurrntUser
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
          var UserEmail=  User.FindFirstValue(ClaimTypes.Email);
            if (UserEmail is null) return BadRequest(new ApiErorrsResponse(StatusCodes.Status400BadRequest));
            var user= await _userManager.FindByEmailAsync(UserEmail);
            if (user is null) return BadRequest(new ApiErorrsResponse(StatusCodes.Status400BadRequest));
            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName
                //Token = await _tokenServices.CreateTokenAsync(user, _userManager)
            });
        }
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmail(string email)
        { 
          var result =await _userService.CheakEmailExitsAsync(email);
            
            return Ok(result);
        }
        [HttpGet("GetCurrentUserAdress")] //Get:/api/Account/GetCurrntUser
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAdress()
        {
         
            var user= await _userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return BadRequest(new ApiErorrsResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<AddressDto>(user.Address));
            
        }
        [HttpPut("Addrees")]
        [Authorize]
        public async Task<IActionResult>Updateaddress(AddressDto address) 
        {

            var user = await _userManager.FindByEmailWithAddressAsync(User);
            var mappaddress = _mapper.Map<Address>(address);
            mappaddress.Id=user.Address.Id;
            user.Address = mappaddress;
           var result= await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiErorrsResponse(400));
            return Ok(address);


        }

        [HttpPut("Addrees1")]
        [Authorize]
          
        public async Task<ActionResult<AddressDto>> Updateaddresss(AddressDto address)
        {
          
            var user = _userService.UpdateAddress(address,User);
            var result = await _userManager.UpdateAsync(user.Result);
            if (!result.Succeeded) return BadRequest(new ApiErorrsResponse(400));
            return Ok(address);


        }



    }
}
