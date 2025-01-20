using E_commerce.core.Models;
using E_Commerce.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public async static Task SeedAsync(StoreDbContext _context)
        {
            if (_context.Brands.Count()==0) {

                var brandsData = File.ReadAllText(@"..\E-Commerce.Repository\Data\DataSeed\brands.json");
                var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);

                if (brands is not null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.Types.Count() == 0)
            {

                var typesData = File.ReadAllText(@"..\E-Commerce.Repository\Data\DataSeed\types.json");
                var types  = JsonSerializer.Deserialize<List<Types>>(typesData);

                if (types is not null && types.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
                
            }
            if (_context.Proudects.Count() == 0)
            {

                var ProudectsData = File.ReadAllText(@"..\E-Commerce.Repository\Data\DataSeed\products.json");
                var proudects = JsonSerializer.Deserialize<List<Proudect>>(ProudectsData);

                if (proudects is not null && proudects.Count() > 0)
                {
                    await _context.Proudects.AddRangeAsync(proudects);
                    await _context.SaveChangesAsync();
                }
            }


        }
    }
}
