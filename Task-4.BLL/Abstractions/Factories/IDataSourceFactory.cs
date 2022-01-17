namespace Task_4.BLL.Abstractions.Factories
{
    public interface IDataSourceFactory<TDtoEntity>
    {
        IFileDataSource<TDtoEntity> CreateInstance(string fileName);
    }
}
