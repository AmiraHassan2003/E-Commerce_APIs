using E_Commerce_APIs.DTO;
using E_Commerce_APIs.Models.Entities;
using E_Commerce_APIs.Repositories.CategoryRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepo;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            categoryRepo = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            List<GetCategoryDto> categoriesDto= categoryRepo.getCategories();
            if(categoriesDto.Count() == 0)
            {
                return BadRequest("Not Found Any Category. :( ");
            }
            return Ok(categoriesDto);
        }

        [HttpGet("withProducts/")]
        public IActionResult GetCategoriesWithProducts()
        {
            List<GetCategoryWithProductDto> categories = categoryRepo.GetCategoriesWithProducts();
            if (categories == null)
            {
                return BadRequest("Not Found Categories :( ");
            }

            return Ok(categories);
        }


        [HttpGet("{id:int}", Name = "CategoryDetails")]
        public IActionResult GetCategoryById(int id)
        {
            GetCategoryWithProductDto category = categoryRepo.GetCategoryById(id);
            if (category == null)
            {
                return BadRequest($"This {id} does`n exist :(");
            }
            return Ok(category);
        }

        [HttpGet("{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            GetCategoryWithProductDto category = categoryRepo.GetCategoryByName(name);
            if (category == null)
            {
                return BadRequest($"This {name} does`n exist :(");
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] GetCategoryWithProductDto DtoCategory)
        {
            if (ModelState.IsValid)
            {
                GetCategoryWithProductDto category = categoryRepo.AddCategory(DtoCategory);
                if(category != null)
                {
                    string url = Url.Link("CategoryDetails", new { id = category.CategoryId});
                    return Created(url, category);
                }
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpPut("{id:int}/{name}")]
        public IActionResult EditCategoryName([FromRoute] int id ,[FromRoute] string name)
        {
            bool isAdded = categoryRepo.EditCategoryName(id, name);
            GetCategoryWithProductDto categoryDto = categoryRepo.GetCategoryById(id);


            if (isAdded != null)
            {
                string url = Url.Link("CategoryDetails", new { id = categoryDto.CategoryId });
                return Created(url, categoryDto);
            }
            return BadRequest($"This {id} doesn`t exist"); 
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveCategory(int id)
        {
            bool isdeleted = categoryRepo.DeleteCategory(id);
            if(isdeleted == false)
            {
                return BadRequest($"this {id} doesn`t exist :( ");
            }
            return Ok("deleted successfully :) ");
        }
    }


}
