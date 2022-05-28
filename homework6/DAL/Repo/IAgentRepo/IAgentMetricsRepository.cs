namespace MManager.Repo
{
    public interface IAgentMetricsRepository<T> where T : class
    {
        List<T> GetAllAgents();
        T GetAgentById(long id);
        void SetNewAgent(T item);
    }
}
