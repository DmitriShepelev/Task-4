using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task_4.Contexts;
using Task_4.DAL.Repositories;
using Task_4.Models;
using Web.Models;
using Manager = Web.Models.Manager;

namespace Web.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationContext db)
        {
            _logger = logger;
            _db = db;
        }

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

            var managersModels = managers.Select(m => new Manager {Id = m.Id, SecondName = m.SecondName}).ToList();
            managersModels.Insert(0, new Manager{Id = 0, SecondName = "Все"});
            var orderViewModel = new AllOrdersViewModel() 
            {
                Orders = orders
            };

            if (managerId != null && managerId > 0)
            {
                orderViewModel.Orders = orders.Where(order => order.Manager.Id == managerId);
            }

            return View(orderViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
