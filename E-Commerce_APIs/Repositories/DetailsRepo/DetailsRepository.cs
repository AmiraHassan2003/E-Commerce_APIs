using E_Commerce_APIs.Models.Context;
using E_Commerce_APIs.Models.Entities;

namespace E_Commerce_APIs.Repositories.DetailsRepo
{
    public class DetailsRepository:IDetailsRepository
    {
        E_CommerceDbContext context;
        public DetailsRepository(E_CommerceDbContext context) 
        {
            this.context = context;
        }
        public string GetProductName(int productId)
        {
            Product product = context.products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return null;
            }
            return product.Name;

        }
        public string GetProductDescription(int productId)
        {
            Product product = context.products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return null;
            }
            return product.Description;
        }
        public double GetProductPrice(int productId)
        {
            Product product = context.products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return -1;
            }
            return product.Price;
        }
        public int GetStockQuantity(int productId)
        {
            Product product = context.products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return -1;
            }
            return product.StockQuantity;
        }
        public int GetQuantitySold(int productId)
        {
            Product product = context.products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return 0;
            }
            return product.QuantitySold;
        }
        
        public string GetCategoryName(int productId)
        {
            Product product = context.products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return null;
            }
            Category category = context.categories.FirstOrDefault(c => c.Id == product.CategoryId);
            
            return category.Name;
        }
        public string ShowImage(int productId)
        {
            Product product = context.products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return null;
            }
            return product.Image;
        }
    }
}
