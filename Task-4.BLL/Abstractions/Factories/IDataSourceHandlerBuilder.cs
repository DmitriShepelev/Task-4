using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions.Factories
{
    public interface IDataSourceHandlerBuilder<DTOEntity>
    {
        IDataSourceHandler Build(IFileDataSource<DTOEntity> source);
    }
}
