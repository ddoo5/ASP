using System.Data.SQLite;
using System.Globalization;
using WorkWithBD;

namespace MetricsAgent
{
	public class HddMetricsRepository : IHddMetricsRepository
	{
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";


        public void Create(HddMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(@value,@time)";
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
            cmd.CommandText = "DELETE FROM hddmetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }


        public void Update(HddMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "UPDATE hddmetrics SET value = @value, time = @time WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", item.Id); cmd.Parameters.AddWithValue("@value", item.Value); cmd.Parameters.AddWithValue("@time", item.Time); cmd.Prepare();
            cmd.ExecuteNonQuery();
        }


        public IList<HddMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics";
            var returnList = new List<HddMetrics>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetrics
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


        public HddMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE id= @id";
            cmd.Parameters.AddWithValue("@id", id);
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new HddMetrics
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

