using E_Commerce_APIs.Models.Entities;
using E_Commerce_APIs.Repositories.DetailsRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly IDetailsRepository detailsRepo;
        public DetailsController(IDetailsRepository detailsRepository)
        {
            detailsRepo = detailsRepository;
        }


        [HttpGet("getProductName/{productId:int}")]
        public IActionResult GetProductName(int productId)
        {
            string name = detailsRepo.GetProductName(productId);
            if (name == null)
            {
                return BadRequest($"this {productId} id doesnt exist :( ");
            }
            return Ok(name);
        }

        [HttpGet("getProductDescription/{productId:int}")]
        public IActionResult GetProductDescription(int productId)
        {
            string description = detailsRepo.GetProductDescription(productId);
            if (description == null)
            {
                return BadRequest($"this {productId} id doesnt exist :( ");
            }
            return Ok(description);
        }

        [HttpGet("getProductPrice/{productId:int}")]
        public IActionResult GetProductPrice(int productId)
        {
            double description = detailsRepo.GetProductPrice(productId);
            if (description == -1)
            {
                return BadRequest($"this {productId} id doesnt exist :( ");
            }
            return Ok(description);
        }


        [HttpGet("getStockQuantity/{productId:int}")]
        public IActionResult GetStockQuantity(int productId)
        {
            int stockQuantity = detailsRepo.GetStockQuantity(productId);
            if (stockQuantity == -1)
            {
                return BadRequest($"this {productId} id doesnt exist :( ");
            }
            return Ok(stockQuantity);
        }

        [HttpGet("getQuantitySold/{productId:int}")]
        public IActionResult GetQuantitySold(int productId)
        {
            int quantitySold = detailsRepo.GetQuantitySold(productId);
            if (quantitySold == -1)
            {
                return BadRequest($"this {productId} id doesnt exist :( ");
            }
            return Ok(quantitySold);
        }

        [HttpGet("getCategoryName/{productId:int}")]
        public IActionResult GetCategoryName(int productId)
        {
            string categoryName = detailsRepo.GetCategoryName(productId);
            if (categoryName == null)
            {
                return BadRequest($"this {productId} id doesnt exist :( ");
            }
            return Ok(categoryName);
        }

        [HttpGet("showImage/{productId:int}")]
        public IActionResult ShowImage(int productId)
        {
            string imagePath = detailsRepo.ShowImage(productId);
            if (imagePath == null)
            {
                return BadRequest($"this {productId} id doesnt exist :( ");
            }
            return Ok(imagePath);
        }
    }
}
