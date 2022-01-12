using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.Persistence.Models;

namespace Task_4.BLL.Abstractions
{
    public class DataSourceDto
    {
        public string ManagerSecondName { get; set; }
        public string PurchaseDate { get; set; }
        public string ClientName { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public bool SessionCompleted { get; set; }
    }
}
