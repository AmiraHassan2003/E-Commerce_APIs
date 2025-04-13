using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_APIs.Models.Entities
{
    public class Customer : User
    {
        //[Required(ErrorMessage = "Phone number is required")]
        [MaxLength(11, ErrorMessage = "Phone number cannot exceed 11 digits. :( ")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be 11 digits. :( ")]
        public string? PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Card number is required")]
        [CreditCard(ErrorMessage = "Invalid credit card number format")]
        [StringLength(19, MinimumLength = 13, ErrorMessage = "Card number must be between 13 and 19 digits")]
        [RegularExpression(@"^\d{13,19}$", ErrorMessage = "Card number must contain digits only")]
        public string? CardNumber { get; set; }

        //[Required(ErrorMessage = "Address is required")]
        [StringLength(255, ErrorMessage = "Address is too long")]
        public string? Address { get; set; }

    }
}
