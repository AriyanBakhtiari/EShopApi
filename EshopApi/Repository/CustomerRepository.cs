using EshopApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace EshopApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private EShopApi_Context _context;
        public CustomerRepository(EShopApi_Context context)
        {
            _context = context;
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return await _context.Customer.FindAsync(customer);
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            var customer = await  GetById(id);
             _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;

        }

        public IEnumerable<Customer> GetAll()
        {
            var test = _context.Customer;
            var test2 = test.ToList();
            return _context.Customer.ToList();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customer.Include(c => c.Orders).SingleOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<int> GetCustomerCount()
        {
            return await _context.Customer.CountAsync();
        }

        public async Task<bool> HasCustomer(int id)
        {
            return await _context.Customer.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
