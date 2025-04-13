

using E_Commerce_APIs.DTO;
using E_Commerce_APIs.Models.Entities;
using Microsoft.AspNetCore.Mvc;
namespace E_Commerce_APIs.Repositories.CategoryRepo
{
    public interface ICategoryRepository
    {
        public List<GetCategoryDto> getCategories();
        public List<GetCategoryWithProductDto> GetCategoriesWithProducts();
        public GetCategoryWithProductDto GetCategoryById(int id);
        public GetCategoryWithProductDto GetCategoryByName(string name);
        public GetCategoryWithProductDto AddCategory(GetCategoryWithProductDto NewCategory);
        public bool EditCategoryName(int id, string name);
        public bool DeleteCategory(int id); 
    }
}
