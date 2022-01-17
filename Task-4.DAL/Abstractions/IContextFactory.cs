using System.Data.Common;
using System.Data.Entity;

namespace Task_4.DAL.Abstractions
{
    public interface IContextFactory
    {
        DbContext CreateInstance(DbConnection connection = null, bool contextOwnsConnection = true);
    }
}
