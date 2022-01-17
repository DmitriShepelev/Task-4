namespace Task_4.BLL.Abstractions.Factories
{
    public interface IDtoParserFactory<TDtoEntity>
    {
        IDtoParser<TDtoEntity> CreateInstance();
    }
}
