using E_Commerce_APIs.DTO;
using E_Commerce_APIs.Models.Entities;

namespace E_Commerce_APIs.Repositories.CarRentalRepo
{
    public interface ICarRentalRepository
    {
        public Customer CreateCar(int customer);
        public GetProductInfoInCarDto AddProductToCar(int customerId, int productId, int quantity);
        public GetProductInfoInCarDto BuyProduct(int customerId, int productId);
        public void DeleteProductFromCar(int customerId, int productId);
        public bool DeleteCar(int customerId);
        public void Checkout(int customerId);
    }
}
