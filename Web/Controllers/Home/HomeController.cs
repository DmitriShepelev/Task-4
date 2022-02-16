using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Task_4.BLL.Services;
using Task_4.DAL.Contexts;
using Task_4.DAL.Models;
using Task_4.DAL.Repositories;
using Web.Models;
//using ManagerViewModel = Web.Models.ManagerViewModel;

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

        [Authorize(Roles = "user")]
        public async Task<IActionResult> Index(int page = 1, SortState sortOrder = SortState.PurchaseDateDesc)
        {
            var service = new OrderService();
            var result = await service.GetOrders(page, sortOrder);
            AllOrdersViewModel viewModel = new()
            {
                PageViewModel = new PageViewModel(service.Count, page, service.PageSize),
                SortViewModel = new SortViewModel(sortOrder),
                Orders = result
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
