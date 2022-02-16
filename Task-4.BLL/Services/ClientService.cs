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
    public class ClientService
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
            var dalClients = await new Repository<Client>().GetAllAsync();

            IQueryable<Manager> dalManagers = null;
            IQueryable<Product> dalProducts = null;

            var dalOrders = new OrderRepository().GetAllOrders();

            #region filter
            switch (clientId)
            {
                case > 0:
                    dalOrders = dalOrders.Where(m => m.ClientId == clientId);
                    dalManagers = dalOrders.AsNoTracking().Where(order => order.ClientId == clientId).Select(order => order.Manager).Distinct();
                    dalProducts = dalOrders.AsNoTracking().Where(order => order.ClientId == clientId).Select(order => order.Product).Distinct();
                    break;
                case 0:
                    dalManagers = dalOrders.AsNoTracking().Select(order => order.Manager).Distinct();
                    dalProducts = dalOrders.AsNoTracking().Select(order => order.Product).Distinct();
                    break;
            }
            #endregion

            if (managerId != null && managerId != 0)
            {
                dalOrders = dalOrders.Where(c => c.ManagerId == managerId);
            }

            if (productId != null && productId != 0)
            {
                dalOrders = dalOrders.Where(p => p.ProductId == productId);
            }

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
            Clients = dalClients.OrderBy(manager => manager.LastName).ToArray();
            if (dalManagers != null) Managers = await dalManagers.OrderBy(client => client.SecondName).ToArrayAsync();
            if (dalProducts != null) Products = await dalProducts.OrderBy(product => product.Name).ToArrayAsync();
            return Orders;
        }
    }
}
