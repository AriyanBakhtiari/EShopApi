using EshopApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EshopApi.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Task<Customer> GetById(int id);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<Customer> DeleteCustomer(int id);
        Task<bool> HasCustomer(int id);
        Task<int> GetCustomerCount();

    }
}
