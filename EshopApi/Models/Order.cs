using System;
using System.Collections.Generic;

namespace EshopApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public double? Total { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public int SalesPersonId { get; set; }

        public Customer Customer { get; set; }
        public SalesPerson SalesPerson { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}