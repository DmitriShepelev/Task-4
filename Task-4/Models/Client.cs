using System.Collections.Generic;

namespace Task_4.Persistence.Models
{
   public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Client() => Orders = new HashSet<Order>();
    }
}
