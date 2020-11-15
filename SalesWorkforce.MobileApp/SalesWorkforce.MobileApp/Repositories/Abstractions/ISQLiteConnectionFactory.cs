using SQLite;

namespace SalesWorkforce.MobileApp.Repositories.Abstractions
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteConnection CreateConnection(string dbName);
    }
}
