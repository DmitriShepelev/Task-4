using System.Collections.Generic;

namespace Task_4.Persistence.Models
{
    public class Manager
    {
        public  int Id { get; set; }
        public string SecondName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Manager() => Orders = new HashSet<Order>();

    }
}
