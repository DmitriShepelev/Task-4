using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task_4.DAL.Models;
using Task_4.DAL.Repositories;
using Web.Models;

namespace Task_4.BLL.Services
{
    public class OrderService
    {
        public int Count { get; set; }
        public ICollection<Order> Orders { get; set; }
        public int PageSize => 10;

        public async Task<IEnumerable<Order>> GetOrders(int page = 1, SortState sortOrder = SortState.PurchaseDateDesc)
        {
            var dalOrders = new OrderRepository().GetAllOrders();

            dalOrders = sortOrder switch
            {
                SortState.ManagerNameDesc => dalOrders.OrderByDescending(order => order.Manager.SecondName),
                SortState.AmountAsc => dalOrders.OrderBy(order => order.Amount),
                SortState.AmountDesc => dalOrders.OrderByDescending(order => order.Amount),
                SortState.ClientNameAsc => dalOrders.OrderBy(order => order.Client.LastName),
                SortState.ClientNameDesc => dalOrders.OrderByDescending(order => order.Client.LastName),
                SortState.ProductNameAsc => dalOrders.OrderBy(order => order.Product.Name),
                SortState.ProductNameDesc => dalOrders.OrderByDescending(order => order.Product.Name),
                SortState.PurchaseDateAsc => dalOrders.OrderBy(order => order.PurchaseDate),
                SortState.PurchaseDateDesc => dalOrders.OrderByDescending(order => order.PurchaseDate),
                _ => dalOrders.OrderBy(order => order.Manager.SecondName)
            };

            var items = dalOrders.Skip((page - 1) * PageSize).Take(PageSize);
            Count = await dalOrders.CountAsync();

            Orders = await items.ToArrayAsync();
            return Orders;
        }
    }
}