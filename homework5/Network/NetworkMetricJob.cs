using Quartz;
using System.Diagnostics;
using WorkWithBD;

namespace MetricsAgent
{
	public class NetworkMetricJob : IJob
	{
		private INetworkMetricsRepository _repository;
		private PerformanceCounter _performanceCounter;

		public NetworkMetricJob(INetworkMetricsRepository repository)
		{
			_repository = repository;
			_performanceCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		}

		public Task Execute(IJobExecutionContext context)
		{
			_repository.Create(new Microsoft.OpenApi.Models.NetworkMetric
			{
				DateTime = DateTime.Now,
				Value = Convert.ToInt32(_performanceCounter.NextValue())
			});
			return Task.CompletedTask;
		}
	}
}

