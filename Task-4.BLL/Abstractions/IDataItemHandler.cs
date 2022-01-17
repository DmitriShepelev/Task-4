using System;

namespace Task_4.BLL.Abstractions
{
   public interface IDataItemHandler<TDtoEntity> : IDisposable
   {
       void SaveOrder(TDtoEntity dtoEntity);
   }
}
