using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_APIs.Models.Entities
{
    public class CarIncludeProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }



        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }



        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }



        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product product { get; set; }



        [ForeignKey("CarId")]
        public int CarId { get; set; }
        public  CarRental car { get; set; }

    }
}
