using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4.DAL.Abstractions
{
    public interface IContextFactory
    {
        DbContext CreateInstance(DbConnection connection = null, bool contextOwnsConnection = true);
    }
}
