//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Task_4.Models;

//namespace Task_4.Contexts
//{
//    public sealed class UserContext : DbContext
//    {
//        public DbSet<User> Users { get; set; }
//        public UserContext(DbContextOptions<UserContext> options)
//            : base(options)
//        {
//            Database.EnsureCreated();
//        }
//    }
//}
