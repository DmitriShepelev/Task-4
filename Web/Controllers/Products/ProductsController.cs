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

namespace Web.Controllers.Products
{
    public class ProductsController : Controller
    {
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Index(int? managerId, int? clientId, int? productId,
            int page = 1, SortState sortOrder = SortState.PurchaseDateDesc)
        {
            var service = new ProductService();
            var result = await service.GetOrders(managerId, clientId, productId, page, sortOrder);

            if (productId is null)
            {
                return View("GetProductId", service.Products);
            }

            ProductOrdersViewModel viewModel = new()
            {
                PageViewModel = new PageViewModel(service.Count, page, service.PageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(
                    service.Clients, clientId,
                    service.Products, productId,
                    service.Managers, managerId),
                Orders = result,
                Products = service.Products
            };

            return View(viewModel);
        }
    }
}
