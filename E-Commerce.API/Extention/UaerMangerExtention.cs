using E_commerce.core.Models.Identity;
using E_Commerce.API.Erorrs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.API.Extention
{
    public static class UaerMangerExtention
    {

        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            if (UserEmail is null) return null;
           var user = await  userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(U => U.Email == UserEmail);
            if (user == null) return null;
            return user;
        }
    }
}
