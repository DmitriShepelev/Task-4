using System.Collections.Generic;
using Task_4.DAL.Models;

namespace Web.Models
{
    public class ClientOrdersViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
