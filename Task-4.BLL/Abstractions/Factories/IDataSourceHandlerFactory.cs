namespace Task_4.BLL.Abstractions.Factories
{
    public interface IDataSourceHandlerFactory<TDtoEntity>
    {
        IDataSourceHandler CreateInstance(IFileDataSource<TDtoEntity> source);
    }
}
