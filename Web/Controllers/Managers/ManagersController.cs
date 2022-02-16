using System;
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

namespace Web.Controllers.Managers
{
    public class ManagersController : Controller
    {
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Index(int? managerId, int? clientId, int? productId,
            int page = 1, SortState sortOrder = SortState.PurchaseDateDesc)
        {
            var service = new ManagerService();
            var result = await service.GetOrders(managerId, clientId, productId, page, sortOrder);

            if (managerId is null)
            {
                return View("GetManagerId", service.Managers);
            }

            ManagerOrdersViewModel viewModel = new()
            {
                PageViewModel = new PageViewModel(service.Count, page, service.PageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(
                    service.Clients, clientId,
                    service.Products, productId,
                    service.Managers, managerId),
                Orders = result,
                Managers = service.Managers
            };

            return View(viewModel);
        }

        public IActionResult EditOrder(int? orderId)
        {
            if (orderId is null)
                return RedirectToAction("Index");
            ViewBag.OrderId = orderId;
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditOrder(OrderModel order)
        {
            if (ModelState.IsValid)
            {
                var dalOrders = await new OrderRepository().GetOrderByIdAsync(order.Id);
                dalOrders.Manager.SecondName = order.Manager;
                dalOrders.Client.LastName = order.Client;
                dalOrders.Product.Name = order.Product;
                dalOrders.PurchaseDate = order.PurchaseDate;
                dalOrders.Amount = Convert.ToDecimal(order.Amount);
                await new OrderRepository().UpdateAsync(dalOrders);
                return RedirectToAction("Index");
            }
            else
            {
                return View(order);
            }
        }
    }
}
