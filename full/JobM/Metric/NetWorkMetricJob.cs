using MetricsAgent.Api;
using MManager.Agent;
using MManager.Client;
using MManager.Models;
using MManager.Repo;
using MManager.Repo.IMetricsRepo;
using Quartz;

namespace MManager.Job
{
    public class NetWorkMetricJob : IJob
    {
        private INetWorkMetricsRepository _repository;
        private IAgentRepository _agentsRepository;
        private IMetricsAgentClient _clientAgent;



        public NetWorkMetricJob(INetWorkMetricsRepository repository, IAgentRepository agentsRepository, IMetricsAgentClient clientAgent)
        {
            _repository = repository;
            _agentsRepository = agentsRepository;
            _clientAgent = clientAgent;
        }



        public Task Execute(IJobExecutionContext context)
        {
            var agents = _agentsRepository.GetAllAgents();

            foreach (AgentModel agent in agents)
            {
                var fromTime = _repository.GetLastMetric(agent.AgentId).ToLocalTime();
                var metrics = _clientAgent.GetNetWorkMetrics(new GetAllNetWorkTrafficMetricsApiRequest()
                {
                    fromTime = fromTime.AddSeconds(1),
                    toTime = DateTimeOffset.UtcNow.ToLocalTime(),
                    ClientBaseAddress = agent.AgentUrl
                });
                if (metrics != null)
                {
                    foreach (var metric in metrics)
                    {
                        _repository.AddMetric(new NetWorkMetric() { Value = metric.Value, Time = metric.Time, agentId = agent.AgentId });
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
