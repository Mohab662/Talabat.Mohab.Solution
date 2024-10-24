using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Orders_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "PaymentSuccesseded")]
        PaymentSuccesseded,
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed
    }
}
