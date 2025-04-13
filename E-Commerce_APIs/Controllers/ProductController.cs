using E_Commerce_APIs.DTO;
using E_Commerce_APIs.Models.Entities;
using E_Commerce_APIs.Repositories.ProductRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepo;
        public ProductController(IProductRepository productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpGet("{categoryId}")]
        public IActionResult GetProducts(int categoryId)
        {
            List<GetCategoryWithProductDto> ProductWithCategoryDto = productRepo.GetProducts(categoryId);
            if (ProductWithCategoryDto.Count() == 0)
            {
                return BadRequest($"The category with ID = {categoryId} doesn't contain any products. :( ");
            }
            return Ok(ProductWithCategoryDto);
        }

        [HttpGet("product/{id:int}", Name = "ProductDetails")]
        public IActionResult GetProductById(int id)
        {
            GetProductsDto productDto = productRepo.GetProductById(id);
            if(productDto == null)
            {
                return BadRequest($"this product not found :( ");
            }
            return Ok(productDto);
        }

        [HttpGet("product/{name}")]
        public IActionResult GetProductByName(string name)
        {
            GetProductsDto productDto = productRepo.GetProductByName(name);
            if (productDto == null)
            {
                return BadRequest($"this product not found :( ");
            }
            return Ok(productDto);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] GetProductsDto productDto)
        {
            bool isAdded = productRepo.AddProduct(productDto);
            if (ModelState.IsValid)
            {
                if (isAdded == true)
                {
                    string url = Url.Link("ProductDetails", new { id = productDto.ProductId });
                    return Created(url, productDto);
                }
                else
                {
                    return BadRequest($"this product not found :( ");
                }
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{ProductId:int}")]
        public IActionResult EditPoduct(int ProductId, [FromBody] GetProductsDto product)
        {
            bool isEdited = productRepo.EditProduct(ProductId, product);
            if (ModelState.IsValid)
            {
                if (isEdited == true)
                {
                    string url = Url.Link("ProductDetails", new { id = ProductId });
                    return Created(url, product);
                }
                else
                {
                    return BadRequest($"this product not found :( ");
                }
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{ProductId:int}")]
        public IActionResult RemoveProduct(int ProductId)
        {
            bool isDeleted = productRepo.RemoveProduct(ProductId);
            if(isDeleted == false)
            {
                return BadRequest($"this {ProductId} id does`nt exist :( ");
            }
            return Ok("Deleted Susccefully :) ");
        }

    }
}
