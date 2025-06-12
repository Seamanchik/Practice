using BelTel.Models;
using System.Data.SQLite;

namespace BelTel.Database
{
    public class DatabaseHelper
    {
        public static List<Document> GetDocuments()
        {
            var list = new List<Document>();

            using (var cmd = new SQLiteCommand("SELECT id, name FROM documents", Database.GetConnection()))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Document
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }

            return list;
        }

        public static List<Recipient> GetRecipients()
        {
            var list = new List<Recipient>();

            using (var cmd = new SQLiteCommand("SELECT id, name FROM recipients", Database.GetConnection()))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Recipient
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }

            return list;
        }

        public static List<Series> GetSeries()
        {
            var list = new List<Series>();

            using (var cmd = new SQLiteCommand("SELECT id, name FROM series", Database.GetConnection()))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Series
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }

            return list;
        }

        public static void AddDocument(string name)
        {
            Database.OpenConnection();
            using var cmd = new SQLiteCommand("INSERT INTO documents (name) VALUES (@name)", Database.GetConnection());
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
            Database.CloseConnection();
        }

        public static void AddRecipient(string name)
        {
            Database.OpenConnection();
            using var cmd = new SQLiteCommand("INSERT INTO recipients (name) VALUES (@name)", Database.GetConnection());
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
            Database.CloseConnection();
        }

        public static void AddSeries(string name)
        {
            Database.OpenConnection();
            using var cmd = new SQLiteCommand("INSERT INTO series (name) VALUES (@name)", Database.GetConnection());
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
            Database.CloseConnection();
        }
    }
}
