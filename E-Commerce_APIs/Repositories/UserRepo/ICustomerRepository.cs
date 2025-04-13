using E_Commerce_APIs.Models.Entities;

namespace E_Commerce_APIs.Repositories.UserRepo
{
    public interface ICustomerRepository
    {
        public bool Signup(Customer customer);
        public bool Login(string email, string password);
        public bool Logout(int customerId);


    }
}
