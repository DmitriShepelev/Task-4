using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_4.Models;

namespace Task_5.Web.Models
{
    public class ClientOrdersViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
