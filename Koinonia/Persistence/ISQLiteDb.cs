using SQLite;

namespace Koinonia
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}