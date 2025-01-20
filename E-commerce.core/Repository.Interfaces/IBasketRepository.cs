using E_commerce.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Repository.Interfaces
{
    public interface IBasketRepository
    {
       Task<UserBasket?> GetBasketAsync(string BasketId);
       Task<UserBasket> UpdateBasketAsync(UserBasket basket);
        Task<bool> DeleteBasketAsync(string BasketId);

    }
}
