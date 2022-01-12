using Task_4.BLL.Abstractions;
using Task_4.DAL.Abstractions;

namespace Task_4.BLL.BusinessLogicUoWs
{
   public class AddUoW<TEntity> : BaseUoW<TEntity>, IAddUoW<TEntity> where TEntity : class
    {
        public AddUoW(IGenericRepository<TEntity> repository) : base(repository)
        {
        }

        public void TakeAction(TEntity entityItem)
        {
            Repository.Add(entityItem);
            Repository.Context.SaveChanges();
        }
    }
}
