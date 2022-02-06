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

namespace Task_5.Web.Controllers.Managers
{
    public class ManagersController : Controller
    {
        public IActionResult Index(int? managerId)
        {
            IEnumerable<Order> orders;
            IEnumerable<Task_4.Models.Manager> managers;

            using (var context = new ApplicationContext())
            {
                context.Set<Order>()
                    .Include(order => order.Client)
                    .Include(order => order.Manager)
                    .Include(order => order.Product)
                    .Load();

                orders = new GenericRepository<Order>(context)
                    .Get()
                    .OrderByDescending(date => date.PurchaseDate)
                    .ToList();

                managers = new GenericRepository<Task_4.Models.Manager>(context)
                    .Get()
                    .ToList();
            }

            var managersModels = managers.Select(m => new Manager { Id = m.Id, SecondName = m.SecondName }).ToList();
            managersModels.Insert(0, new Manager { Id = 0, SecondName = "Все" });
            var orderViewModel = new ManagerOrdersViewModel()
            {
                //Clients = clients, 
                Managers = managersModels,
                //Products = products, 
                Orders = orders
            };

            if (managerId != null && managerId > 0)
            {
                orderViewModel.Orders = orders.Where(order => order.Manager.Id == managerId);
            }

            return View(orderViewModel);
        }
    }
}
