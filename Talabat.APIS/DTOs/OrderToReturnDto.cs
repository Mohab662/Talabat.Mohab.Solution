﻿using Talabat.Core.Entities.Orders_Aggregate;

namespace Talabat.APIS.DTOs
{
    public class OrderToReturnDto
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrederDate { get; set; } = DateTimeOffset.UtcNow;
        public string Status { get; set; }
        public Address ShppingAddress { get; set; }

        public string? DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

        public decimal SubTotal { get; set; }
         public decimal Total { get; set; }

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}