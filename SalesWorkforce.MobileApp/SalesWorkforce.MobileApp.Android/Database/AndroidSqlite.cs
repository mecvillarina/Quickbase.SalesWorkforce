using SalesWorkforce.MobileApp.Repositories.Abstractions;
using SQLite;
using System.IO;

namespace SalesWorkforce.MobileApp.Droid.Database
{
    public class AndroidSqlite : ISQLiteConnectionFactory
    {
        public SQLiteConnection CreateConnection(string dbName)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, dbName);
            return new SQLiteConnection(path);
        }
    }
}