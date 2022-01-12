using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.DAL.Abstractions
{
    public interface IConnectionFactory
    {
        DbConnection CreateInstance(bool openOnCreate = false);
    }
}
