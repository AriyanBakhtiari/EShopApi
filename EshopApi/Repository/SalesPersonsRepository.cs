using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopApi.Repository;
using EshopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EshopApi.Repositories
{
    public class SalesPersonRepository:ISalesPersonRepository
    {
        private EShopApi_Context _context;

        public SalesPersonRepository(EShopApi_Context context)
        {
            _context = context;
        }

        public IEnumerable<SalesPerson> GetAll()
        {
            return _context.SalesPerson.ToList();
        }

        public async Task<SalesPerson> Add(SalesPerson sales)
        {
            await _context.SalesPerson.AddAsync(sales);
            await _context.SaveChangesAsync();
            return sales;
        }

        public async Task<SalesPerson> Find(int id)
        {
            return await _context.SalesPerson.SingleOrDefaultAsync(s => s.SalesPersonId == id);

        }

        public async Task<SalesPerson> Update(SalesPerson sales)
        {
            _context.Update(sales);
            await _context.SaveChangesAsync();
            return sales;
        }

        public async Task<SalesPerson> Remove(int id)
        {
            var sales = await _context.SalesPerson.SingleAsync(s => s.SalesPersonId == id);
            _context.SalesPerson.Remove(sales);
            await _context.SaveChangesAsync();
            return sales;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.SalesPerson.AnyAsync(s => s.SalesPersonId == id);
        }
    }
}
