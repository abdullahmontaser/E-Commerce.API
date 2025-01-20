using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Models.Order
{
    public class OrderItem:BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProudectItemOrder proudect, decimal price, int quantity)
        {
            Proudect = proudect;
            Price = price;
            Quantity = quantity;
        }

        public ProudectItemOrder Proudect  { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
