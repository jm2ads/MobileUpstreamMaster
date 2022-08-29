using SQLite;

namespace Frontend.Data.Commons
{
    public interface IConnectionFactory
    {
        SQLiteAsyncConnection GetConnection { get; }
    }
}
