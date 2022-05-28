using Dapper;
using MManager.DateHand;
using MManager.Models;
using MManager.Repo.IMetricsRepo;
using System.Data.SQLite;

namespace MManager.Repo.MetricsRepo
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;";



        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }



        public void AddMetric(HddMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO hddmetrics (value, time, agentId) VALUES(@value, @time, @agentId)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    agentId = item.agentId
                });
            }
        }


        public IList<HddMetric> GetMetricsByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE Time >= @fromTime AND Time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
            }
        }


        public IList<HddMetric> GetMetricsFromAllCluster()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT Id, Time, Value, agentId FROM hddmetrics").ToList();
            }
        }


        public IList<HddMetric> GetAgentMetricById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE Id = @id",
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
                return connection.QueryFirstOrDefault<DateTimeOffset>("SELECT MAX(Time) FROM hddmetrics WHERE agentId = @id", new { id = id });
            }
        }
    }
}
