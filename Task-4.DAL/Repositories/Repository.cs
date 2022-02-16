using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task_4.DAL.Abstractions;
using Task_4.DAL.Contexts;
using Task_4.DAL.Models;

namespace Task_4.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : HaveId

    {
        public async Task<ICollection<T>> GetAllAsync()
        {
            await using var context = new ApplicationContext();
            ICollection<T> result = await context
                .Set<T>()
                .AsNoTracking()
                .ToArrayAsync();

            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            await using var context = new ApplicationContext();
            var result = await context
                .Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await using var context = new ApplicationContext();
            var result = context.Update(entity).Entity;

            await context.SaveChangesAsync();

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = new ApplicationContext();
            var entity = await context
                .Set<T>()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entity == default)
            {
                throw new KeyNotFoundException();
            }

            context.Remove(entity);

            await context.SaveChangesAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await using var context = new ApplicationContext();
            var addResult = await context.AddAsync(entity);
            var result = addResult.Entity;

            await context.SaveChangesAsync();

            return result;
        }
    }
}
