using Dapper;
using MManager.DateHand;
using MManager.Models;
using MManager.Repo.IMetricsRepo;
using System.Data.SQLite;

namespace MManager.Repo.MetricsRepo
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;";



        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }



        public void AddMetric(RamMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO rammetrics (value, time, agentId) VALUES(@value, @time, @agentId)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    agentId = item.agentId
                });
            }
        }


        public IList<RamMetric> GetMetricsByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE Time >= @fromTime AND Time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }


        public IList<RamMetric> GetMetricsFromAllCluster()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics").ToList();
            }
        }


        public IList<RamMetric> GetAgentMetricById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE Id = @id",
                    new
                    {
                        id = id
                    }).ToList();
            }
        }


        public DateTimeOffset GetLastMetric(long id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QueryFirstOrDefault<DateTimeOffset>("SELECT MAX(Time) FROM rammetrics WHERE agentId = @id", new { id = id });
            }
        }
    }
}
