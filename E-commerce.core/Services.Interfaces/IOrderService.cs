using E_commerce.core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliverymethodId, Address ShipingAddress);
        Task<IReadOnlyList<Order>> GetOrderForSpecifcUser(string BuyerEmail);
        Task<Order?> GetOrderByIdForSpecifcUser(string BuyerEmail,int OrderId);

    }
}
