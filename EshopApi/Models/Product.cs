using System.Collections.Generic;
using System.Security.Principal;

namespace EshopApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Size { get; set; }
        public string Varienty { get; set; }
        public double? Price { get; set; }
        public string Status { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}