using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_4.BLL.Services;
using Task_4.DAL.Contexts;
using Task_4.DAL.Repositories;
using Task_4.DAL.Models;
using Web.Models;
using Client = Task_4.DAL.Models.Client;

namespace Web.Controllers.Clients
{
    public class ClientsController : Controller
    {
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Index(int? managerId, int? clientId, int? productId,
            int page = 1, SortState sortOrder = SortState.PurchaseDateDesc)
        {
            var service = new ClientService();
            var result = await service.GetOrders(managerId, clientId, productId, page, sortOrder);

            if (clientId is null)
            {
                return View("GetClientId", service.Clients);
            }

            ClientOrdersViewModel viewModel = new()
            {
                PageViewModel = new PageViewModel(service.Count, page, service.PageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(
                    service.Clients, clientId,
                    service.Products, productId,
                    service.Managers, managerId),
                Orders = result,
                Clients = service.Clients
            };

            return View(viewModel);
        }
    }
}
