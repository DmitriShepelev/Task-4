using Task_4.BLL.Abstractions;
using Task_4.BLL.Infrastructure;
using Task_4.Models;

namespace Task_4.BLL.DataSources
{
  public  class DtoParser : IDtoParser<DataSourceDto>
    {
        private const int FirstNameIndex = 0;
        private const int LastNameIndex = 1;
        public Client Client { get; set; }
        public Product Product { get; set; }
        public Manager Manager { get; set; }
        public Order Order { get; set; }
        public void Parse(DataSourceDto item)
        {
            var name = item.ClientName.Split(" ");
            Client = new Client() {FirstName = name[FirstNameIndex], LastName = name[LastNameIndex] };
            Product = new Product(){Name = item.ProductName};
            Manager = new Manager() {SecondName = item.ManagerSecondName};
            Order = new Order()
            {
                Client = Client, Manager = Manager, Product = Product, PurchaseDate = item.PurchaseDate,
                Amount = item.Amount
            };
        }
    }
}
