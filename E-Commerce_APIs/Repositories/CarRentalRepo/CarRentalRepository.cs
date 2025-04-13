using E_Commerce_APIs.DTO;
using E_Commerce_APIs.Models.Context;
using E_Commerce_APIs.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Repositories.CarRentalRepo
{
    public class CarRentalRepository: ICarRentalRepository
    {
        private E_CommerceDbContext context;
        public CarRentalRepository(E_CommerceDbContext context)
        {
            this.context = context;   
        }

        public Customer CreateCar(int customerId)
        {
            var customer = context.customers.FirstOrDefault(x => x.Id == customerId)
                ?? throw new Exception("Customer not found.");

            var existingCar = context.cars.FirstOrDefault(c => c.CustomerId == customerId);
            if (existingCar != null && existingCar.Status == false)
            {
                throw new Exception("Cart already exists for this customer.");
            }

            var carRental = new CarRental
            {
                Status = false,
                CustomerId = customerId,
                TotalAmount = 0
            };

            context.cars.Add(carRental);
            context.SaveChanges();

            return customer;
        }


        public GetProductInfoInCarDto AddProductToCar(int customerId, int productId, int quantity)
        {
            try
            {
                var customer = context.customers.FirstOrDefault(c => c.Id == customerId);
                if (customer == null)
                    throw new Exception($"Customer with ID {customerId} not found.");

                var product = context.products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                    throw new Exception($"Product with ID {productId} not found.");

                var car = context.cars.FirstOrDefault(c => c.CustomerId == customerId);
                if (car == null)
                {
                    car = new CarRental { CustomerId = customerId };
                    context.cars.Add(car);
                    context.SaveChanges();
                }

                if(quantity < 1)
                {
                    throw new Exception("Please provide a valid quantity greater than 0.");
                }

                var carIncludeProduct = context.carIncludeProducts
                    .FirstOrDefault(c => c.ProductId == productId && c.CarId == car.Id);

                if (carIncludeProduct == null)
                {
                    carIncludeProduct = new CarIncludeProduct
                    {
                        CarId = car.Id,
                        ProductId = productId,
                        Quantity = quantity,
                        Price = product.Price * quantity
                    };
                    context.carIncludeProducts.Add(carIncludeProduct);
                }
                else
                {
                    carIncludeProduct.Quantity += quantity;
                    carIncludeProduct.Price += carIncludeProduct.Quantity * product.Price;
                }

                context.SaveChanges();

                return new GetProductInfoInCarDto
                {
                    CustomerId = customerId,
                    CustomerName = customer.UserName,
                    CarId = car.Id,
                    ProductId = productId,
                    ProductName = product.Name,
                    Quantity = carIncludeProduct.Quantity
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product to cart: {ex.Message}");
                return null;
            }
        }


        public GetProductInfoInCarDto BuyProduct(int customerId, int productId)
        {
            try { 
                Customer customer = context.customers.FirstOrDefault(c => c.Id == customerId);
                if (customer == null) 
                { 
                    throw new Exception($"Customer with ID {customerId} not found. :("); 
                }
                Product product = context.products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {productId} not found. :(");
                }

                CarRental car = context.cars.FirstOrDefault(c => c.CustomerId == customerId);
                if (car == null)
                {
                    throw new Exception($"No cart found for customer ID {customerId}. :(");
                }

                CarIncludeProduct cip = context.carIncludeProducts
                    .FirstOrDefault(c => c.CarId == car.Id &&  c.ProductId == productId);
                if (cip == null) 
                {
                    throw new Exception($"Product ID {productId} is not in the cart.");
                }
            
                if (product.StockQuantity < cip.Quantity)
                {
                    throw new Exception($"Not enough stock for product '{product.Name}'. Only {product.StockQuantity} left.");
                }
                
                product.StockQuantity -= cip.Quantity;
                product.QuantitySold += cip.Quantity;

                context.carIncludeProducts.Remove(cip);
                context.SaveChanges();

                return new GetProductInfoInCarDto()
                {
                    CustomerId = customerId,
                    CustomerName = customer.UserName,
                    ProductId = productId,
                    ProductName = product.Name,
                    CarId = car.Id,
                    Quantity = cip.Quantity,
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public void DeleteProductFromCar(int customerId, int productId)
        {
            var customer = context.customers.FirstOrDefault(c => c.Id == customerId)
                ?? throw new Exception("Customer not found.");

            var product = context.products.FirstOrDefault(p => p.Id == productId)
                ?? throw new Exception("Product not found.");

            var car = context.cars.FirstOrDefault(c => c.CustomerId == customerId)
                ?? throw new Exception("Customer does not have a car.");

            var cip = context.carIncludeProducts
                .FirstOrDefault(c => c.CarId == car.Id && c.ProductId == productId)
                ?? throw new Exception("Product not found in the customer's cart.");

            context.carIncludeProducts.Remove(cip);
            context.SaveChanges();
        }

        public bool DeleteCar(int customerId)
        {
            var customer = context.customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                throw new Exception($"Customer with ID {customerId} does not exist.");
            }

            var car = context.cars.FirstOrDefault(c => c.CustomerId == customerId);
            if (car == null)
            {
                return false;
            }

            context.cars.Remove(car);
            context.SaveChanges();

            return true;
        }



        public void Checkout(int customerId)
        {
            var customer = context.customers.FirstOrDefault(c => c.Id == customerId)
                ?? throw new Exception("Customer not found.");

            var car = context.cars.FirstOrDefault(c => c.CustomerId == customerId)
                ?? throw new Exception("No cart found for this customer.");

            List <CarIncludeProduct> carIncludeProducts = context.carIncludeProducts
                .Where(c => c.CarId == car.Id)
                .ToList();

            if (carIncludeProducts == null || carIncludeProducts.Count == 0)
                throw new Exception("Cart is already empty.");

            foreach(CarIncludeProduct product in carIncludeProducts)
            {
                this.BuyProduct(customerId, product.ProductId);
            }
            
            car.Status = true;
            context.SaveChanges();
        }

    }
}
