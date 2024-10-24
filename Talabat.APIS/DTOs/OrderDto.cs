using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Orders_Aggregate;

namespace Talabat.APIS.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        public AddressDto ShippingAddress { get; set; }
        [Required]
        public int DelivaeryMethodId { get; set; }

    }
}
