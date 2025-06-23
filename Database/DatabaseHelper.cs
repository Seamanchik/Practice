using Practice.Models;
using Practice.ViewModels;
using System.Data.SQLite;

namespace Practice.Database
{
    public class DatabaseHelper
    {
        public static List<Document> GetDocuments()
        {
            var list = new List<Document>();

            Database.OpenConnection();
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

            Database.CloseConnection();
            return list;
        }

        public static List<Recipient> GetRecipients()
        {
            var recipients = new List<Recipient>();
            const string query = "SELECT id, name FROM recipients";

            Database.OpenConnection();
            using var cmd = new SQLiteCommand(query, Database.GetConnection());
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                recipients.Add(new Recipient
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"].ToString()!
                });
            }

            Database.CloseConnection();
            return recipients;
        }

        public static List<Series> GetSeries()
        {
            var list = new List<Series>();

            Database.OpenConnection();
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

            Database.CloseConnection();
            return list;
        }

        public static Blank? GetBlankByNumber(int blankNumber)
        {
            Blank? blank = null;
            const string query = "SELECT id, number_blank, box_id, date, recipient_id, product_name FROM blanks WHERE number_blank = @number";

            Database.OpenConnection();

            using var cmd = new SQLiteCommand(query, Database.GetConnection());
            cmd.Parameters.AddWithValue("@number", blankNumber);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                blank = new Blank
                {
                    Id = Convert.ToInt32(reader["id"]),
                    BlankNumber = Convert.ToInt32(reader["number_blank"]),
                    BoxId = Convert.ToInt32(reader["box_id"]),
                    Date = reader["date"] == DBNull.Value ? null : DateTime.Parse(reader["date"].ToString()!),
                    RecipientId = reader["recipient_id"] == DBNull.Value ? null : Convert.ToInt32(reader["recipient_id"]),
                    ProductName = reader["product_name"]?.ToString()
                };
            }

            Database.CloseConnection();
            return blank;
        }

        public static List<BlankViewModel> GetBlanksByBox(BoxViewModel box)
        {
            var list = new List<BlankViewModel>();

            Database.OpenConnection();

            var command = new SQLiteCommand(@"
        SELECT 
            b.number_blank,
            b.date,
            b.product_name,
            r.name AS recipient_name,
            x.series,
            d.name AS document_name
        FROM blanks b
        JOIN box x ON b.box_id = x.id
        JOIN documents d ON x.document_id = d.id
        LEFT JOIN recipients r ON b.recipient_id = r.id
        WHERE b.number_blank BETWEEN @start AND @end
          AND x.series = @series
          AND d.name = @docName
        ORDER BY b.number_blank;
    ", Database.GetConnection());

            command.Parameters.AddWithValue("@start", box.StartNumber);
            command.Parameters.AddWithValue("@end", box.EndNumber);
            command.Parameters.AddWithValue("@series", box.Series);
            command.Parameters.AddWithValue("@docName", box.DocumentName);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new BlankViewModel
                {
                    BlankNumber = Convert.ToInt32(reader["number_blank"]),
                    Date = reader["date"] != DBNull.Value ? DateTime.Parse(reader["date"].ToString()) : (DateTime?)null,
                    ProductName = reader["product_name"]?.ToString(),
                    RecipientName = reader["recipient_name"]?.ToString(),
                    Series = reader["series"]?.ToString(),
                    DocumentName = reader["document_name"]?.ToString()
                });
            }

            Database.CloseConnection();
            return list;
        }

        public static List<BoxViewModel> GetBoxes()
        {
            var boxes = new List<BoxViewModel>();

            Database.OpenConnection();

            var command = new SQLiteCommand(@"
    SELECT 
        MIN(b.number_blank) AS start_number,
        MAX(b.number_blank) AS end_number,
        x.series,
        d.name AS document_name
    FROM box x
    JOIN blanks b ON b.box_id = x.id
    JOIN documents d ON x.document_id = d.id
    GROUP BY x.id, x.series, d.name
    ORDER BY MIN(b.number_blank);
", Database.GetConnection());

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                boxes.Add(new BoxViewModel
                {
                    StartNumber = Convert.ToInt32(reader["start_number"]),
                    EndNumber = Convert.ToInt32(reader["end_number"]),
                    Series = reader["series"].ToString(),
                    DocumentName = reader["document_name"].ToString()
                });
            }

            Database.CloseConnection();
            return boxes;
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

        public static void AddBoxWithBlanks(int startNumber, int endNumber, string series, int documentId)
        {
            Database.OpenConnection();
            using var transaction = Database.GetConnection().BeginTransaction();

            try
            {
                var insertBox = new SQLiteCommand("INSERT INTO box (start_number, end_number, series, document_id) VALUES (@start, @end, @series, @docId); SELECT last_insert_rowid();", Database.GetConnection());
                insertBox.Parameters.AddWithValue("@start", startNumber);
                insertBox.Parameters.AddWithValue("@end", endNumber);
                insertBox.Parameters.AddWithValue("@series", series);
                insertBox.Parameters.AddWithValue("@docId", documentId);

                long boxId = (long)insertBox.ExecuteScalar();

                for (int i = startNumber; i <= endNumber; i++)
                {
                    var insertBlank = new SQLiteCommand("INSERT INTO blanks (number_blank, box_id, date, recipient_id, product_name) VALUES (@number, @boxId, NULL, NULL, NULL);", Database.GetConnection());
                    insertBlank.Parameters.AddWithValue("@number", i);
                    insertBlank.Parameters.AddWithValue("@boxId", boxId);
                    insertBlank.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Ошибка при добавлении коробки и бланков: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Database.CloseConnection();
            }
        }

        public static void UpdateBlankDateInDatabase(int blankId, string selectedDate)
        {
            Database.OpenConnection();

            var cmd = new SQLiteCommand("UPDATE blanks SET date = @date WHERE id = @id", Database.GetConnection());
            cmd.Parameters.AddWithValue("@date", selectedDate);
            cmd.Parameters.AddWithValue("@id", blankId);

            cmd.ExecuteNonQuery();

            Database.CloseConnection();
        }

        public static void UpdateBlankProductNameByNumber(int blankNumber, string productName)
        {
            const string query = "UPDATE blanks SET product_name = @productName WHERE number_blank = @number";

            Database.OpenConnection();

            using var cmd = new SQLiteCommand(query, Database.GetConnection());
            cmd.Parameters.AddWithValue("@productName", productName);
            cmd.Parameters.AddWithValue("@number", blankNumber);

            cmd.ExecuteNonQuery();

            Database.CloseConnection();
        }

        public static void UpdateBlankRecipientByNumber(int blankNumber, int recipientId)
        {
            const string query = "UPDATE blanks SET recipient_id = @recipientId WHERE number_blank = @number";

            Database.OpenConnection();

            using var cmd = new SQLiteCommand(query, Database.GetConnection());
            cmd.Parameters.AddWithValue("@recipientId", recipientId);
            cmd.Parameters.AddWithValue("@number", blankNumber);
            cmd.ExecuteNonQuery();

            Database.CloseConnection();
        }

        public static bool AreBlanksNumberRangeAvailable(int startNumber, int endNumber)
        {
            Database.OpenConnection();

            var command = new SQLiteCommand(@"
        SELECT COUNT(*) 
        FROM blanks 
        WHERE number_blank BETWEEN @start AND @end;
    ", Database.GetConnection());

            command.Parameters.AddWithValue("@start", startNumber);
            command.Parameters.AddWithValue("@end", endNumber);

            var count = Convert.ToInt32(command.ExecuteScalar());

            Database.CloseConnection();

            return count == 0;
        }
    }
}