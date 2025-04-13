using E_Commerce_APIs.DTO;
using E_Commerce_APIs.Models.Context;
using E_Commerce_APIs.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Repositories.ProductRepo
{
    public class ProductRepository: IProductRepository
    {
        E_CommerceDbContext context;
        public ProductRepository(E_CommerceDbContext productContext) 
        {
            this.context = productContext;
        }

        public List<GetCategoryWithProductDto> GetProducts(int categoryId)
        {
            List<GetCategoryWithProductDto> ProductWithCategoryDto = context.categories.
                Include(c => c.products).
                Where(c => c.Id == categoryId).
                Select(c => new GetCategoryWithProductDto
                {
                    CategoryId = c.Id,
                    CategoryName = c.Name,
                    products = c.products.Select(p => new GetProductsDto
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                        Price = p.Price,
                        ProductDesc = p.Description,
                        StockQuantity = p.StockQuantity,
                        QuantitySold = p.QuantitySold,
                        Image = p.Image,
                        ProductCategoryId = categoryId
                    }).ToList()
                }).ToList();
            
            return ProductWithCategoryDto;

        }



        public GetProductsDto GetProductById(int ProductId)
        {
            Product product = context.products.Include(c => c.category).FirstOrDefault(p => p.Id == ProductId);
            if (product == null) 
            {
                return null;
            }
            GetProductsDto productDto = new GetProductsDto
            {
                ProductId = product.Id,
                ProductCategoryId = product.category.Id,
                //CategoryName = product.category.Name,
                ProductDesc = product.Description,
                Image = product.Image,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ProductName = product.Name,
                QuantitySold = product.QuantitySold
            };
            //productDto.ProductId = product.Id;
            return productDto;
        }

        public GetProductsDto GetProductByName(string ProductName)
        {
            ProductName = ProductName.ToLower();
            Product product = context.products.Include(c => c.category).FirstOrDefault(p => p.Name == ProductName);
            if (product == null)
            {
                return null;
            }
            GetProductsDto productDto = new GetProductsDto
            {
                ProductId = product.Id,
                ProductCategoryId = product.category.Id,
                //CategoryName = product.category.Name,
                ProductDesc = product.Description,
                Image = product.Image,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ProductName = product.Name,
                QuantitySold = product.QuantitySold
            };
            return productDto;
        }

        public bool AddProduct(GetProductsDto productDto)
        {
            Category category = context.categories.FirstOrDefault(c => c.Id == productDto.ProductCategoryId);
            if(category == null || productDto == null)
            {
                return false;
            }

            if(productDto.Price < 1)
            {
                return false;
            }
            
            Product product = new Product
            {
                Name = productDto.ProductName,
                Image = productDto.Image,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                QuantitySold = 0,
                Description = productDto.ProductDesc,
                CategoryId = productDto.ProductCategoryId,
            };

            context.products.Add(product);
            context.SaveChanges();
            productDto.ProductId = product.Id;
            return true;
        }


        public bool EditProduct(int id, GetProductsDto updatedProduct)
        {
            Product existingProduct = context.products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                return false;

            if (!string.IsNullOrWhiteSpace(updatedProduct.ProductName))
                existingProduct.Name = updatedProduct.ProductName;

            if (!string.IsNullOrWhiteSpace(updatedProduct.ProductDesc))
                existingProduct.Description = updatedProduct.ProductDesc;

            if (!string.IsNullOrWhiteSpace(updatedProduct.Image))
                existingProduct.Image = updatedProduct.Image;

            if (updatedProduct.Price > 0)
                existingProduct.Price = updatedProduct.Price;

            if (updatedProduct.StockQuantity >= 0)
                existingProduct.StockQuantity = updatedProduct.StockQuantity;

            if (updatedProduct.QuantitySold >= 0)
                existingProduct.QuantitySold = updatedProduct.QuantitySold;

            if (updatedProduct.ProductCategoryId != 0)
                existingProduct.CategoryId = updatedProduct.ProductCategoryId;

                context.SaveChanges();
            return true;
        }

        public bool RemoveProduct(int id)
        {
            Product product = context.products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return false;
            }
            context.Remove(product);
            context.SaveChanges();
            return true;
        }
    }
}
