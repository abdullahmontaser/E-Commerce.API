using E_commerce.core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Specifiction.Order
{
    public class OrderWithPaymentSpecifiction :BaseSpecifictin<Models.Order.Order, int>
    {
        public OrderWithPaymentSpecifiction(string PaymentIntentId):base(O=>O.PaymentIntentId==PaymentIntentId)
        {
            
        }
    }
}
