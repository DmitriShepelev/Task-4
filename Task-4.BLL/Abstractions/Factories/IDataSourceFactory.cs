using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.BLL.Abstractions.Factories
{
    public interface IDataSourceFactory<TDtoEntity>
    {
        IFileDataSource<TDtoEntity> CreateInstance(string fileName);
    }
}
