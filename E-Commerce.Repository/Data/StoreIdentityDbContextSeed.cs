using E_commerce.core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public static class StoreIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Email = "tarekatef@gmail.com",
                    DisplayName = "tarekatef",
                    UserName = "tarek.atef",
                    PhoneNumber = "01016189965",
                    Address = new Address()
                    {
                        FirstName = "tarek",
                        LastName = "Atef",
                        City = "Salem",
                        Country = "Cairo",
                        Street = "Sbiko"
                    }
                };
                await _userManager.CreateAsync(user, "P@ssW0rd");
            }
        }
    }
}
