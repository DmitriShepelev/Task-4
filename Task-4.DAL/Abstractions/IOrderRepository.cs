using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Models;

namespace Task_4.DAL.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<ICollection<Order>> GetAllByClientAsync(int clientId);
        Task<ICollection<Order>> GetAllByManagerAsync(int managerId);
        Task<ICollection<Order>> GetAllByProductAsync(int productId);
        Task<Order> GetOrderByIdAsync(int orderId);

    }
}
