using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Price Must Be At Least 0.1 !")]
        public decimal Price { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Categoey { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity Must Be At Least 1 !")]
        public int Quantity { get; set; }
    }
}