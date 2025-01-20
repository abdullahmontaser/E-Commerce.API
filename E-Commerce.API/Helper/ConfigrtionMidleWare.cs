using E_Commerce.API.Midlleware;
using E_Commerce.Repository.Data.Contexts;
using E_Commerce.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using E_commerce.core.Models.Identity;
using E_Commerce.Repository;

namespace E_Commerce.API.Helper
{
    public static class ConfigrtionMidleWare
    {
        public static async Task<WebApplication> ConfigrtionMidleWareAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var logerfactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
                await DeliveryMethodSeed.SeedDeliveryAsync(context);
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager);
            }
            catch (Exception ex)
            {

                var logger = logerfactory.CreateLogger<Program>();
                logger.LogError(ex, "there is error migration");
            }
            //exption server
            app.UseMiddleware<ExceptionMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // not found end point
            app.UseStatusCodePagesWithRedirects("/erorr/{0}");

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();


            return app;
        }
    }
}
