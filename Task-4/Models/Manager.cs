using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
