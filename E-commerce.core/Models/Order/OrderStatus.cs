using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.core.Models.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value =("Panding"))]
        Panding,
        [EnumMember(Value = ("Payment Recevied"))]
        PaymentRecevied,
        [EnumMember(Value = ("Payment Failed"))]
        PaymentFailed,
    }
}
