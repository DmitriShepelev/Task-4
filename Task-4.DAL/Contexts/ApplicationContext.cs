using Microsoft.EntityFrameworkCore;
using Task_4.DAL.Models;

namespace Task_4.DAL.Contexts
{
    public sealed class ApplicationContext : DbContext
    {
        private static bool _isEnsured;
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ApplicationContext()
        {
            if (_isEnsured) return;
            Database.EnsureCreated();
            _isEnsured = true;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=task4_db_test;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
               .HasMany(x => x.Orders);

            modelBuilder.Entity<Manager>()
                .HasMany(x => x.Orders);

            modelBuilder.Entity<Product>()
                .HasMany(x => x.Orders);
        }
    }
}
