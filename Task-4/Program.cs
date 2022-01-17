using System;
using System.Data.Entity;
using Task_4.Persistence.Contexts;
using Task_4.Persistence.Models;

namespace Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Client client = new Client()
            {
                FirstName = "Dima",
                LastName = "Shepelev"
            };

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Task4Context>());
            Task4Context context = new Task4Context();
            context.Clients.Add(client);
            context.SaveChanges();
        }
    }
}
