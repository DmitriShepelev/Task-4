namespace Task_4.BLL.Abstractions
{
   public interface IDbConnectionHandler
    {
        void Commit(bool sessionCompletedState);
        void Rollback(bool sessionCompletedState);
    }
}
