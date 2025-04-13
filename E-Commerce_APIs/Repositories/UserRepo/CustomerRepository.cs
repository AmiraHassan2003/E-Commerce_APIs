using E_Commerce_APIs.Models.Context;
using E_Commerce_APIs.Models.Entities;
using E_Commerce_APIs.Repositories.CarRentalRepo;

namespace E_Commerce_APIs.Repositories.UserRepo
{
    public class CustomerRepository: ICustomerRepository
    {
        private E_CommerceDbContext context;
        private ICarRentalRepository carRepo;
        public CustomerRepository(E_CommerceDbContext context)
        {
            this.context = context;
        }

        public bool Signup(Customer customer)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Add(customer);
                    context.SaveChanges();

                    carRepo.CreateCar(customer.Id);
                    //transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();

                    Console.WriteLine($"Signup failed: {ex.Message}");

                    throw new Exception("Signup failed: " + ex.Message, ex);
                }
            }
        }


        public bool Login(string email, string password)
        {
            try
            {
                var customer = context.customers.FirstOrDefault(c => c.Email == email && c.Password == password);

                if (customer == null)
                {
                    return false;
                }
                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        public bool Logout(int customerId)
        {
            try
            {
                var customer = context.customers.FirstOrDefault(c => c.Id == customerId);
                if (customer == null)
                {
                    return false;
                }

                var car = context.cars.FirstOrDefault(c => c.CustomerId == customerId);
                if (car != null)
                {
                    context.cars.Remove(car);
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
