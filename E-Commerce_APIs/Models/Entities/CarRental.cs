using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_APIs.Models.Entities
{
    public class CarRental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Car date is required")]
        //public string CarDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero")]
        public double TotalAmount { get; set; }


        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer customer { get; set; }
    }
}
