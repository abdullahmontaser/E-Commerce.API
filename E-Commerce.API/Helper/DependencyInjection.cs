using E_commerce.core;
using E_commerce.core.Mapping;
using E_commerce.core.Models.Identity;
using E_commerce.core.Repository.Interfaces;
using E_commerce.core.Services.Interfaces;
using E_Commerce.API.Erorrs;
using E_Commerce.Repository;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Data.Contexts;
using E_Commerce.Repository.Repository;
using E_Commerce.Services.Services.Caches;
using E_Commerce.Services.Services.Proudects;
using E_Commerce.Services.Services.Tokens;
using E_Commerce.Services.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using E_Commerce.Services.Services.Order;
using E_Commerce.Services.Services.Payment;

namespace E_Commerce.API.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDebndancy(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddBultiIn();
            services.AddSwager();
            services.AddDbContext(configuration);
            services.AddUserService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvalidModelStateResponse();
            services.AddReidsServices(configuration);
            services.AddIdentityServices();
            services.AddCors(configuration);
            services.AddAuthenticationServices(configuration);
            


            return services;
        
        }  
        private static IServiceCollection AddBultiIn(this IServiceCollection services) 
        {
            services.AddControllers();
        
            return services;
        }  
        private static IServiceCollection AddSwager(this IServiceCollection services) 
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        } 
        private static IServiceCollection AddCors(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", Options =>
                {
                    Options.AllowAnyHeader();
                    Options.AllowAnyMethod();
                    Options.WithOrigins(configuration["FrontBaseUrl"]);
                });
            });
            return services;
        }    
        private static IServiceCollection AddDbContext(this IServiceCollection services,IConfiguration configuration) 
        {
           services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });   
           
            return services;
        }
        private static IServiceCollection AddUserService(this IServiceCollection services)
        {
            
            services.AddScoped<IProdectService, ProudectServicers>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, CashesService>();
            services.AddScoped<ITokenServices, TokenService >();
            services.AddScoped<IUserService, UserService >();
            services.AddScoped<IOrderService, OrderService >();
            services.AddScoped<IPaymentService, PaymentService >();
            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(M => M.AddProfile(new ProudectProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile()));
            

            return services;
        }  
        private static IServiceCollection ConfigureInvalidModelStateResponse(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var erorrs = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage).ToArray();
                    var response = new ApiValidationErorrsResponse()
                    {
                        Erorrs = erorrs
                    };
                    return new BadRequestObjectResult(response);
                };
            });

           

            return services;
        }
        private static IServiceCollection AddReidsServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((servicesProvider) =>
            {
              var Connection =  configuration.GetConnectionString("Rides");
                return ConnectionMultiplexer.Connect(Connection);

            });


            return services;
        }
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                     .AddEntityFrameworkStores<StoreDbContext>();
            return services;
        }
        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
 
            return services;
        }
    }
}
