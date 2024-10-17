using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",ErrorMessage ="InValid Password")]
        public string Password { get; set; }
    }
}
