using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task_4.DAL.Models;
using Task_4.DAL.Repositories;
using Web.Models;

namespace Task_4.BLL.Services
{
    public class ManagerService
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
            var dalManagers = await new Repository<Manager>().GetAllAsync();

            IQueryable<Client> dalClients = null;
            IQueryable<Product> dalProducts = null;

            var dalOrders = new OrderRepository().GetAllOrders();

            switch (managerId)
            {
                case > 0:
                    dalOrders = dalOrders.Where(m => m.ManagerId == managerId);
                    dalClients = dalOrders.AsNoTracking().Where(order => order.ManagerId == managerId).Select(order => order.Client).Distinct();
                    dalProducts = dalOrders.AsNoTracking().Where(order => order.ManagerId == managerId).Select(order => order.Product).Distinct();
                    break;
                case 0:
                    dalClients = dalOrders.AsNoTracking().Select(order => order.Client).Distinct();
                    dalProducts = dalOrders.AsNoTracking().Select(order => order.Product).Distinct();
                    break;
            }

            if (clientId != null && clientId != 0)
            {
                dalOrders = dalOrders.Where(c => c.ClientId == clientId);
            }

            if (productId != null && productId != 0)
            {
                dalOrders = dalOrders.Where(p => p.ProductId == productId);
            }

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
            Managers = dalManagers.OrderBy(manager=>manager.SecondName).ToArray();
            if (dalClients != null) Clients = await dalClients.OrderBy(client => client.LastName).ToArrayAsync();
            if (dalProducts != null) Products = await dalProducts.OrderBy(product => product.Name).ToArrayAsync();
            return Orders;
        }
    }
}
