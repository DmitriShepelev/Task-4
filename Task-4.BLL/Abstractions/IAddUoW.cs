namespace Task_4.BLL.Abstractions
{
    public interface IAddUoW<TEntity> : ISingleEntityUoW<TEntity> where TEntity : class
    {
        void TakeAction(TEntity entityItem);
    }
}
