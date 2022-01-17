using System.Data.Common;

namespace Task_4.DAL.Abstractions
{
    public interface IConnectionFactory
    {
        DbConnection CreateInstance(bool openOnCreate = false);
    }
}
