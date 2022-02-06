using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_4.Models;

namespace Task_5.Web.Models
{
    public class AllOrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
