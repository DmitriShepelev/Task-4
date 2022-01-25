using System.Collections.Generic;

namespace Task_4.Models
{
    public sealed class Manager
    {
        public  int Id { get; set; }
        public string SecondName { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Manager() => Orders = new HashSet<Order>();

    }
}
