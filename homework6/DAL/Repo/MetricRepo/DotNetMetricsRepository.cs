using Dapper;
using MManager.DateHand;
using MManager.Models;
using MManager.Repo.IMetricsRepo;
using System.Data.SQLite;

namespace MManager.Repo.MetricsRepo
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;";



        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }



        public void AddMetric(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO dotnetmetrics (value, time, agentId) VALUES(@value, @time, @agentId)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    agentId = item.agentId
                });
            }
        }



        public IList<DotNetMetric> GetMetricsByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE Time >= @fromTime AND Time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }


        public IList<DotNetMetric> GetMetricsFromAllCluster()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics").ToList();
            }
        }


        public IList<DotNetMetric> GetAgentMetricById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE Id = @id",
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
                return connection.QueryFirstOrDefault<DateTimeOffset>("SELECT MAX(Time) FROM dotnetmetrics WHERE agentId = @id", new { id = id });
            }
        }
    }
}
