using System.Data.SQLite;

namespace Practice.Database
{
    public class Database
    {
        private static SQLiteConnection? _connection;

        public static SQLiteConnection GetConnection()
        {
            if (_connection == null )
            {
                string path = $"Data Source={Path.Combine(Application.StartupPath, "beltel.db")};Version=3;";

                _connection = new SQLiteConnection(path);
            }

            return _connection;
        }

        public static void OpenConnection()
        {
            if (_connection == null)
                GetConnection();

            if (_connection?.State != System.Data.ConnectionState.Open)
                _connection!.Open();
        }

        public static void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}
