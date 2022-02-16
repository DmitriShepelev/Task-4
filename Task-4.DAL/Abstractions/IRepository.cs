using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task_4.DAL.Models;

namespace Task_4.DAL.Abstractions
{
    public interface IRepository<T>  where  T : HaveId
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> AddAsync(T entity);
    }
}
