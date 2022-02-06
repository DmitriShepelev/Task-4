using System.Collections.Generic;
using Task_4.Models;

namespace Web.Models
{
    public class ClientOrdersViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
