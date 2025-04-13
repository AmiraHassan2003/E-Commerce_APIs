
using E_Commerce_APIs.DTO;
using E_Commerce_APIs.Models.Context;
using E_Commerce_APIs.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Repositories.CategoryRepo
{
    public class CategoryRepository : ICategoryRepository
    {
        E_CommerceDbContext context;

        public CategoryRepository(E_CommerceDbContext context)
        {
            this.context = context;   
        }

        public List<GetCategoryDto> getCategories()
        {
            
            List<GetCategoryDto> categories = context.categories
                .Select(c => new GetCategoryDto
                {
                    CategoryId = c.Id,
                    CategoryName = c.Name,
                })
                .ToList();
            return categories;
        }

        public List<GetCategoryWithProductDto> GetCategoriesWithProducts()
        {
            List<GetCategoryWithProductDto> categories = context.categories
                .Include(c => c.products)
                .Where(c => c.products.Any())
                .Select(c => new GetCategoryWithProductDto
                {
                    CategoryId = c.Id,
                    CategoryName = c.Name,
                    products = c.products.Select(p => new GetProductsDto
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                        ProductDesc = p.Description,
                        StockQuantity = p.StockQuantity,
                        QuantitySold = p.QuantitySold,
                        Price = p.Price,
                        Image = p.Image,
                    }).ToList()
                }).ToList();

            return categories;
        }

        public GetCategoryWithProductDto GetCategoryById(int id)
        {
            Category category = context.categories.Include(c => c.products).FirstOrDefault(c => c.Id == id);
            if(category == null)
            {
                return null;
            }
            GetCategoryWithProductDto categoryDto = new GetCategoryWithProductDto
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                products = category.products.Select(p => new GetProductsDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Price = p.Price,
                    ProductDesc = p.Description,
                    StockQuantity = p.StockQuantity,
                    QuantitySold = p.QuantitySold,
                    Image = p.Image,
                }).ToList()
            };

            return categoryDto;
        }

        public GetCategoryWithProductDto GetCategoryByName(string name)
        {
            Category category = context.categories.Include(c => c.products).FirstOrDefault(c => c.Name == name);
            if(category == null)
            {
                return null;
            }
            GetCategoryWithProductDto categoryDto = new GetCategoryWithProductDto
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                products = category.products.Select(p => new GetProductsDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Price = p.Price,
                    ProductDesc = p.Description,
                    StockQuantity = p.StockQuantity,
                    QuantitySold = p.QuantitySold,
                    Image = p.Image,
                }).ToList()
            };

            return categoryDto;
        }

        public GetCategoryWithProductDto AddCategory(GetCategoryWithProductDto categoryDto)
        {
            if (categoryDto == null)
            {
                return null;
            }
            Category category = new Category
            {
                Name = categoryDto.CategoryName,
                products = categoryDto.products.Select(p => new Product
                {
                    Name = p.ProductName,
                    Price = p.Price,
                    Description = p.ProductDesc,
                    StockQuantity = p.StockQuantity,
                    QuantitySold = p.QuantitySold,
                    Image = p.Image
                }).ToList()
            };
            context.categories.Add(category);
            context.SaveChanges();
            categoryDto.CategoryId = category.Id;

            return categoryDto;
        }


        public bool EditCategoryName(int id, string name)
        {
            Category category = context.categories.Include(c => c.products).FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return false;
            }

            category.Name = name;

            context.SaveChanges();
            return true;
        }


        public bool DeleteCategory(int id)
        {
            Category category = context.categories.Include(c => c.products).FirstOrDefault(c => c.Id == id);
            if(category == null)
            {
                return false;
            }
            context.Remove(category);
            context.SaveChanges();
            return true;
        }

        

        

        
    }
}
