using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopApi.Repository;
using EshopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EshopApi.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private EShopApi_Context _context;

        public ProductRepository(EShopApi_Context context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Product.ToList();
        }

        public async Task<Product> Add(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Find(int id)
        {
            return await _context.Product.SingleOrDefaultAsync(c => c.ProductId == id);

        }

        public async Task<Product> Update(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Remove(int id)
        {
            var product = await _context.Product.SingleAsync(c => c.ProductId == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Product.AnyAsync(c => c.ProductId == id);
        }
    }
}
