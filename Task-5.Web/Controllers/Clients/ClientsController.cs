using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task_4.Contexts;
using Task_4.DAL.Repositories;
using Task_4.Models;
using Task_5.Web.Models;
using Manager = Task_5.Web.Models.Manager;
using Client = Task_4.Models.Client;

namespace Task_5.Web.Controllers.Clients
{
    public class ClientsController : Controller
    {
        public IActionResult Index(int? clientId)
        {
            IEnumerable<Order> orders;
            IEnumerable<Task_4.Models.Client> clients;

            using (var context = new ApplicationContext())
            {
                context.Set<Order>()
                    .Include(order => order.Client)
                    .Include(order => order.Manager)
                    .Include(order => order.Product)
                    .Load();

                orders = new GenericRepository<Order>(context)
                    .Get()
                    .OrderBy(order => order.Client.LastName)
                    .ToList();

                clients = new GenericRepository<Client>(context)
                    .Get()
                    .ToList();
            }

            var clientsModel = clients.Select(client => new Models.Client { Id = client.Id, Name = client.Name }).ToList();
            clientsModel.Insert(0, new Models.Client() { Id = 0, Name = "Все" });

            var orderViewModel = new ClientOrdersViewModel()
            {
                Clients = clientsModel, 
                //Managers = managersModels,
                //Products = products, 
                Orders = orders
            };

            if (clientId != null && clientId > 0)
            {
                orderViewModel.Orders = orders.Where(order => order.Client.Id == clientId);
            }

            return View(orderViewModel);
        }
    }
}
