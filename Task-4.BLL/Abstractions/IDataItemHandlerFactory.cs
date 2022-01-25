namespace Task_4.BLL.Abstractions
{
    public interface IDataItemHandlerFactory<TDtoEntity>
    {
        public IDataItemHandler<TDtoEntity> CreateInstance();
    }
}
