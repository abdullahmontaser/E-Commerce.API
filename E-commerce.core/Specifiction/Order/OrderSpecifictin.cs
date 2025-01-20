using E_commerce.core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Specifiction.Order
{
    public class OrderSpecifictin:BaseSpecifictin<E_commerce.core.Models.Order.Order,int>
    {
        public OrderSpecifictin(string email,int id):base(p => p.BuyerEmail == email&&p.Id==id)
        {
            Inclouds.Add(O => O.DeliveryMethod);
            Inclouds.Add(O => O.OrderItems);
            
        }
        public OrderSpecifictin(string email): base(p=>p.BuyerEmail==email)
        {
            Inclouds.Add(O => O.DeliveryMethod);
            Inclouds.Add(O => O.OrderItems);
            OrderByDec=O=>O.OrderDate;
        }
    }
}
