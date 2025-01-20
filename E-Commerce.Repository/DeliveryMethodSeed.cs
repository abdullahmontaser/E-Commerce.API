using E_commerce.core.Models;
using E_commerce.core.Models.Order;
using E_Commerce.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository
{
    public class DeliveryMethodSeed
    {
        public async static Task SeedDeliveryAsync(StoreDbContext _context)
        {
            if (_context.DeliveryMethods.Count() == 0)
            {
                var fileData = File.ReadAllText(@"..\E-Commerce.Repository\Data\DataSeed\delivery.json");
                var file= JsonSerializer.Deserialize<List<DeliveryMethod>>(fileData);
                if (file is not null && file.Count() > 0)
                {
                    await _context.DeliveryMethods.AddRangeAsync(file);
                    await _context.SaveChangesAsync();
                }

            }
        }
    }
}
