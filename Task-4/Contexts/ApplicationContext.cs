using Microsoft.EntityFrameworkCore;
using Task_4.Models;

namespace Task_4.Contexts
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            //Database.MigrateAsync();
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=task4_db_test;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(x => x.Id);//.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Client>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Client>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Client>()
                .Property(c => c.Name).HasComputedColumnSql("FirstName + ' ' + LastName");
            modelBuilder.Entity<Client>()  
               .HasMany(x => x.Orders);//.WithMany(x => x.Client);

            modelBuilder.Entity<Manager>()
                .HasKey(x => x.Id);//.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Manager>()
                .Property(x => x.SecondName).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<Manager>()
                .HasMany(x => x.Orders);//.WithRequired(x => x.Manager);

            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);//.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>()
                .Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>()
                .HasMany(x => x.Orders);//.WithRequired(x => x.Product);
            // base.OnModelCreating(modelBuilder);
        }
    }
}
