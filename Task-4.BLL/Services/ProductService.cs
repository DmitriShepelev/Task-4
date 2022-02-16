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
    public class ProductService
    {
        public int Count { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<Manager> Managers { get; set; }
        public ICollection<Product> Products { get; set; }
        public int PageSize => 10;

        public async Task<IEnumerable<Order>> GetOrders(int? managerId, int? clientId, int? productId, int page = 1,
            SortState sortOrder = SortState.PurchaseDateDesc)
        {
            var dalProducts = await new Repository<Product>().GetAllAsync();

            IQueryable<Client> dalClients = null;
            IQueryable<Manager> dalManagers = null;

            var dalOrders = new OrderRepository().GetAllOrders();

            #region filter
            switch (productId)
            {
                case > 0:
                    dalOrders = dalOrders.Where(order => order.ProductId == productId);
                    dalClients = dalOrders.AsNoTracking().Where(order => order.ProductId == productId).Select(order => order.Client).Distinct();
                    dalManagers = dalOrders.AsNoTracking().Where(order => order.ProductId == productId).Select(order => order.Manager).Distinct();
                    break;
                case 0:
                    dalClients = dalOrders.AsNoTracking().Select(order => order.Client).Distinct();
                    dalManagers = dalOrders.AsNoTracking().Select(order => order.Manager).Distinct();
                    break;
            }

            if (clientId != null && clientId != 0)
            {
                dalOrders = dalOrders.Where(c => c.ClientId == clientId);
            }

            if (managerId != null && managerId != 0)
            {
                dalOrders = dalOrders.Where(p => p.ManagerId == managerId);
            }
            #endregion

            #region sort
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
            #endregion

            var items = dalOrders.Skip((page - 1) * PageSize).Take(PageSize);
            Count = await dalOrders.CountAsync();

            Orders = await items.ToArrayAsync();
            Products = dalProducts.OrderBy(product => product.Name).ToArray();
            if (dalClients != null) Clients = await dalClients.OrderBy(client => client.LastName).ToArrayAsync();
            if (dalManagers != null) Managers = await dalManagers.OrderBy(manager => manager.SecondName).ToArrayAsync();
            return Orders;
        }
    }
}
