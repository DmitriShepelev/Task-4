using System.Collections.Generic;
using Task_4.DAL.Models;

namespace Web.Models
{
    public class AllOrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
