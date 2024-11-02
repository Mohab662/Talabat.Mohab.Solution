using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders_Aggregate;

namespace Talabat.Core.Specification.OrderSpecifications
{
    public class OrderSpecifications:BaseSpecification<Order>
    {
        public OrderSpecifications(string buyerEmail):base(o=>o.BuyerEmail== buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            AddOrderBy(o => o.OrederDate);
        }

        public OrderSpecifications(int id, string buyerEmail) : base(o => o.BuyerEmail == buyerEmail&& o.Id == id)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            AddOrderBy(o => o.OrederDate);
        }

    }
}
