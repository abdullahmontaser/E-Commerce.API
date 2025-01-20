
using E_commerce.core;
using E_commerce.core.Mapping;
using E_commerce.core.Services.Interfaces;
using E_Commerce.API.Erorrs;
using E_Commerce.API.Helper;
using E_Commerce.API.Midlleware;
using E_Commerce.Repository;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Data.Contexts;
using E_Commerce.Services.Services.Proudects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDebndancy(builder.Configuration);


         
            var app = builder.Build();
            //Migration Update with code
          await app.ConfigrtionMidleWareAsync();

            app.Run();
        }
    }
}
