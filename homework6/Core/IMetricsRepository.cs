namespace MManager.Repo
{
     public interface IMetricsRepository<T> where T : class
    {
        void AddMetric(T item);
        IList<T> GetAgentMetricById(int id);
        IList<T> GetMetricsFromAllCluster();
        IList<T> GetMetricsByTimePeriod(DateTimeOffset item, DateTimeOffset item2);
        DateTimeOffset GetLastMetric(long id);
    }
}
