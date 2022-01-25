using System.Collections.Generic;

namespace Task_4.Models
{
    public sealed class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Product() => Orders = new HashSet<Order>();
    }
}
