using E_Commerce_APIs.Models.Entities;
using E_Commerce_APIs.Repositories.UserRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepo;
        public CustomerController(ICustomerRepository customerRepository)
        {
            customerRepo = customerRepository;
        }


        [HttpPost("signup")]
        public IActionResult Signup(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("This Customer desn`t exist. :(");
            }
            customerRepo.Signup(customer);
            return Ok(customer);
        }


        [HttpPost("login/{email}/{password}")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                bool loginSuccess = customerRepo.Login(email, password);

                if (loginSuccess)
                {
                    return Ok(new { Message = "Login successful." });
                }
                else
                {
                    return Unauthorized(new { Message = "Invalid email or password." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, new { Message = "An error occurred during login." });
            }
        }


        [HttpDelete("logout/{customerId:int}")]
        public IActionResult Logout(int customerId)
        {
            try
            {
                bool logoutSuccess = customerRepo.Logout(customerId);

                if (logoutSuccess)
                {
                    return Ok(new { Message = "Logout successful." });
                }
                else
                {
                    return NotFound(new { Message = "No car found for this customer or invalid logout operation." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, new { Message = "An error occurred during logout." });
            }
        }

    }
}
