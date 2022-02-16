using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task_4.DAL.Models;

namespace Web.Models
{
    public class FilterViewModel
    {
        public SelectList Managers { get; }
        public SelectList Clients { get; }
        public SelectList Products { get; }
        public int? SelectedManager { get; }
        public int? SelectedClient { get; }
        public int? SelectedProduct { get; }
        public FilterViewModel(
            IEnumerable<Client> clients, int? client,
            IEnumerable<Product> products, int? product,
            IEnumerable<Manager> managers, int? manager
        )
        {
            clients = clients.Prepend(new Client {Id = 0, LastName = "Все"});
            Clients = new SelectList(clients, "Id", "LastName", client);
            SelectedClient = client;

            products = products.Prepend(new Product{ Id = 0, Name = "Все" });
            Products = new SelectList(products, "Id", "Name", product);
            SelectedProduct = product;

           managers = managers.Prepend( new Manager(){Id = 0, SecondName = "Все"});
            Managers = new SelectList(managers, "Id", "SecondName", manager);
            SelectedManager = manager;
        }
    }
}
