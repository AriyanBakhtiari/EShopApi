using Microsoft.EntityFrameworkCore;

namespace EshopApi.Models
{
    public class EShopApi_Context : DbContext
    {

        public EShopApi_Context()
        {
        }
        
        public EShopApi_Context(DbContextOptions<EShopApi_Context> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SalesPerson> SalesPerson { get; set; }

        
    }
}