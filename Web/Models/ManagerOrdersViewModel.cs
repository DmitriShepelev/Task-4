using System.Collections.Generic;
using System.Linq;
using Task_4.DAL.Models;

namespace Web.Models
{
    public class ManagerOrdersViewModel
    {
        public IEnumerable<Manager> Managers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
