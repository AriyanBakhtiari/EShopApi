using EshopApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace EshopApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private EShopApi_Context _context;
        private IMemoryCache _cache;
        public CustomerRepository(EShopApi_Context context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task AddCustomer(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            
                await _context.SaveChangesAsync();
            
            
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
            return _context.Customer.ToList();
        }

        public async Task<Customer> GetById(int id)
        {
            var customer = _cache.Get<Customer>(id);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                 customer = await _context.Customer.Include(c => c.Orders).SingleOrDefaultAsync(c => c.CustomerId == id);
                var cacheoption = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(customer.CustomerId , customer , cacheoption);
                return customer;
            }
            
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
