using System.Collections.Generic;
using Task_4.Models;

namespace Web.Models
{
    public class ManagerOrdersViewModel
    {
        public IEnumerable<Manager> Managers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
