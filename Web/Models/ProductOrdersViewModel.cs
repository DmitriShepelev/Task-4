using System.Collections.Generic;
using Task_4.Models;

namespace Web.Models
{
    public class ProductOrdersViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
