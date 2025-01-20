using E_commerce.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Services.Interfaces
{
    public interface IPaymentService
    {
        //Create or Update PaymentIntentId
        Task<UserBasket?> CreateOrUpdatePaymentIntent(string BasketId);
    }
}
