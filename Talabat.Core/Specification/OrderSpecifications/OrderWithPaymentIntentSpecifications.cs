using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders_Aggregate;

namespace Talabat.Core.Specification.OrderSpecifications
{
    public class OrderWithPaymentIntentSpecifications:BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpecifications(string paymentId):base(o=>o.PaymentIntentId==paymentId)
        {
            
        }
    }
}
