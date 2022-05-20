using System.Data.SQLite;
using System.Globalization;
using WorkWithBD;

namespace MetricsAgent
{
	public class DotNetMetricsRepository : IDotNetMetricsRepository
	{
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";


        public void Create(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(@value,@time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.ToString("s", CultureInfo.GetCultureInfo("ru-RU")));
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "DELETE FROM dotnetmetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }


        public void Update(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "UPDATE dotnetmetrics SET value = @value, time = @time WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", item.Id); cmd.Parameters.AddWithValue("@value", item.Value); cmd.Parameters.AddWithValue("@time", item.Time); cmd.Prepare();
            cmd.ExecuteNonQuery();
        }


        public IList<DotNetMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM dotnetmetrics";
            var returnList = new List<DotNetMetrics>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetDateTime(2)
                    });
                    Console.WriteLine(returnList[^1].Time);
                }
            }
            return returnList;
        }


        public DotNetMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM dotnetmetrics WHERE id= @id";
            cmd.Parameters.AddWithValue("@id", id);
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new DotNetMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTime.Now - TimeSpan.FromDays(reader.GetInt32(1))   //иного решения не нашел
                    };
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

