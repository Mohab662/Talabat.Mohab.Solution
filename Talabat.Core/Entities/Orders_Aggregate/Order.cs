using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Orders_Aggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, ShipAddress shppingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShppingAddress = shppingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrederDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        public ShipAddress ShppingAddress { get; set; }
        //public int DeliveryMethodId { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }
       // public decimal Total { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public string? PaymentIntentId { get; set; } = string.Empty;

    }
}
