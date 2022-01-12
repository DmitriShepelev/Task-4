using System;
using Task_4.BLL.Abstractions;
using Task_4.Persistence.Models;

namespace Task_4.BLL.Handlers
{
    public class DataItemHandler<TDtoEntity> : IDataItemHandler<TDtoEntity>
    {
        protected IDtoParser<TDtoEntity> DtoParser;
        protected IUpsertUoW<Client> ClientUoW;
        protected IUpsertUoW<Manager> ManagerUoW;
        protected IUpsertUoW<Product> ProductUoW;
        protected IAddUoW<Order> OrderUoW;

        public DataItemHandler(IDtoParser<TDtoEntity> dtoParser,
            IUpsertUoW<Client> clientUoW,
            IUpsertUoW<Manager> managerUoW,
            IUpsertUoW<Product> productUoW,
            IAddUoW<Order> orderUoW)
        {
            DtoParser = dtoParser;
            ClientUoW = clientUoW;
            ManagerUoW = managerUoW;
            ProductUoW = productUoW;
            OrderUoW = orderUoW;
        }

        public void SaveOrder(TDtoEntity dtoEntity)
        {
            DtoParser.Parse(dtoEntity);

            var client = DtoParser.Client = ClientUoW.TakeAction(
                x => x.FirstName == DtoParser.Client.FirstName && x.LastName == DtoParser.Client.LastName,
                new Client() { FirstName = DtoParser.Client.FirstName, LastName = DtoParser.Client.LastName }
            );
            var product = DtoParser.Product = ProductUoW.TakeAction(
                x => x.Name == DtoParser.Product.Name,
                new Product() { Name = DtoParser.Product.Name }
            );
            var manager = DtoParser.Manager = ManagerUoW.TakeAction(
                x => x.SecondName == DtoParser.Manager.SecondName,
                new Manager() { SecondName = DtoParser.Manager.SecondName }
            );

            OrderUoW.TakeAction(DtoParser.Order);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            ClientUoW?.Dispose();
            ManagerUoW?.Dispose();
            ProductUoW?.Dispose();
            OrderUoW?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
