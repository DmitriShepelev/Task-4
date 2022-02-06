using System.Collections.Generic;
using Task_4.Models;

namespace Web.Models
{
    public class AllOrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
