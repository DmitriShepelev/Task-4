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
using Product = Task_5.Web.Models.Product;

namespace Task_5.Web.Controllers.Products
{
    public class ProductsController : Controller
    {
        public IActionResult Index(int? productsId)
        {
            IEnumerable<Order> orders;
            IEnumerable<Task_4.Models.Product> products;

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

                products = new GenericRepository<Task_4.Models.Product>(context)
                    .Get()
                    .ToList();
            }

            var productsModels = products.Select(product => new Product { Id = product.Id, Name = product.Name }).ToList();
            productsModels.Insert(0, new Product { Id = 0, Name = "Все" });
            var orderViewModel = new ProductOrdersViewModel()
            {
                //Clients = clients, 
                //Managers = productsModels,
                Products = productsModels, 
                Orders = orders
            };

            if (productsId != null && productsId > 0)
            {
                orderViewModel.Orders = orders.Where(order => order.Product.Id == productsId);
            }

            return View(orderViewModel);
        }
    }
}
