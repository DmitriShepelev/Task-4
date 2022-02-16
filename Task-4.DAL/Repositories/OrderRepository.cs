using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task_4.DAL.Abstractions;
using Task_4.DAL.Contexts;
using Task_4.DAL.Models;

namespace Task_4.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            await using var context = new ApplicationContext();
            return  await context
                .Orders
                
                .AsTracking()
                //.Where(order => order.ClientId == orderId)
                .Include(order => order.Client)
                .Include(order => order.Manager)
                .Include(order => order.Product).
                FirstOrDefaultAsync(order => order.Id == orderId);
               // .Load();
        }

        public async Task<ICollection<Order>> GetAllByClientAsync(int clientId)
        {
            await using var context = new ApplicationContext();
            return await context
                .Orders
                .AsNoTracking()
                .Where(order => order.ClientId == clientId)
                .Include(order => order.Client)
                .Include(order => order.Manager)
                .Include(order => order.Product)
                .ToArrayAsync();
        }

        public async Task<ICollection<Order>> GetAllByManagerAsync(int managerId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Order>> GetAllByManagerAsync(int? managerId = null)
        {
            await using var context = new ApplicationContext();
            return managerId == null ?
             await context
                .Orders
                .AsNoTracking()
                .Include(order => order.Client)
                .Include(order => order.Manager)
                .Include(order => order.Product)
                .ToArrayAsync() :

            await context
                 .Orders
                 .AsNoTracking()
                 .Where(order => order.ManagerId == managerId)
                 .Include(order => order.Client)
                 .Include(order => order.Manager)
                 .Include(order => order.Product)
                 .ToArrayAsync();

            //return await context
            //    .Orders
            //    .AsNoTracking()
            //    .Where(order => order.ManagerId == managerId)
            //    .Include(order => order.Client)
            //    .Include(order => order.Manager)
            //    .Include(order => order.Product)
            //    .ToArrayAsync();
        }

        public async Task<ICollection<Order>> GetAllByProductAsync(int productId)
        {
            await using var context = new ApplicationContext();
            return await context
                .Orders
                .AsNoTracking()
                .Where(order => order.ProductId == productId)
                .Include(order => order.Client)
                .Include(order => order.Manager)
                .Include(order => order.Product)
                .ToArrayAsync();
        }

        public IQueryable<Order> GetAllOrders()
        {
             var context = new ApplicationContext();
            return context.Orders
                .Include(order => order.Client)
                .Include(order => order.Manager)
                .Include(order => order.Product);
        }

    }
}
