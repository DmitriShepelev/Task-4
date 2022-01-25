using System.Collections.Generic;

namespace Task_4.Models
{
   public sealed class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Client() => Orders = new HashSet<Order>();
    }
}
