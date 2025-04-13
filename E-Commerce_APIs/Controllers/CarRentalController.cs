using E_Commerce_APIs.DTO;
using E_Commerce_APIs.Models.Entities;
using E_Commerce_APIs.Repositories.CarRentalRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly ICarRentalRepository carRentalRepo;
        public CarRentalController(ICarRentalRepository carRentalRepository)
        {
            carRentalRepo = carRentalRepository;
        }

        [HttpPost("create-car/{customerId:int}")]
        public IActionResult CreateCar(int customerId)
        {
            Customer customer = carRentalRepo.CreateCar(customerId);
            if (customer == null)
            {
                return BadRequest("This Customer doesnt exist. :( ");
            }
            return Ok(customer);
        }

        [HttpPost("add-product-to-car/{customerId:int}/{productId:int}/{quantity:int}")]
        public IActionResult AddProductToCar(int customerId, int productId, int quantity)
        {
            try
            {
                var productInCar = carRentalRepo.AddProductToCar(customerId, productId, quantity);

                if (productInCar == null)
                {
                    return BadRequest("Product not found in the car or out of stock.");
                }

                return Ok(productInCar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("buy-product/{customerId:int}/{productId:int}")]
        public IActionResult BuyProduct(int customerId, int productId)
        {
            try
            {
                var productInCar = carRentalRepo.BuyProduct(customerId, productId);

                if (productInCar == null)
                {
                    return BadRequest("Product not found in the car or out of stock.");
                }

                return Ok(productInCar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpDelete("delete-from-cart/{customerId:int}/{productId:int}")]
        public IActionResult DeleteProductFromCart(int customerId, int productId)
        {
            try
            {
                carRentalRepo.DeleteProductFromCar(customerId, productId);
                return Ok("Product deleted from cart.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-car/{customerId:int}")]
        public IActionResult DeleteCar(int customerId)
        {
            try
            {
                bool isDeleted = carRentalRepo.DeleteCar(customerId);

                if (isDeleted)
                {
                    return Ok(new { Message = "Car deleted successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Car not found for this customer." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the car.", Details = ex.Message });
            }
        }


        [HttpDelete("checkout/{customerId:int}")]
        public IActionResult Checkout(int customerId)
        {
            try
            {
                carRentalRepo.Checkout(customerId);
                return Ok("Product deleted from cart.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
