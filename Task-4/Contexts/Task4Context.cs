using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.Persistence.Models;

namespace Task_4.Contexts
{
    public class Task4Context : DbContext
    {
        private readonly DbConnection _connection;

        public Task4Context() : base("Task4Db")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }

        public Task4Context(DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection)
        {
            _connection = connection;
            Database.CreateIfNotExists();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Client>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Client>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Client>()
               .HasMany(x => x.Orders).WithRequired(x => x.Client);

            modelBuilder.Entity<Manager>()
                .HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Manager>()
                .Property(x => x.SecondName).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<Manager>()
                .HasMany(x => x.Orders).WithRequired(x => x.Manager);

            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>()
                .Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>()
                .HasMany(x => x.Orders).WithRequired(x => x.Product);
            // base.OnModelCreating(modelBuilder);
        }
    }
}
