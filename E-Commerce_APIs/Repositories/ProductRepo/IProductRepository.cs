using E_Commerce_APIs.DTO;
using E_Commerce_APIs.Models.Entities;

namespace E_Commerce_APIs.Repositories.ProductRepo
{
    public interface IProductRepository
    {
        public List<GetCategoryWithProductDto> GetProducts(int categoryId);
        public GetProductsDto GetProductById(int ProductId);
        public GetProductsDto GetProductByName(string ProductName);
        public bool AddProduct(GetProductsDto product);
        public bool EditProduct(int id, GetProductsDto Product);
        public bool RemoveProduct(int id);
    }
}
