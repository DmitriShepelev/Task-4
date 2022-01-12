namespace Task_4.BLL.Abstractions.Factories
{
    public interface IDataItemHandlerFactory<TDtoEntity>
    {
        public IDataItemHandler<TDtoEntity> CreateInstance();
    }
}
