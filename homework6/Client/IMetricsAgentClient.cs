using MetricsAgent.Api;

namespace MManager.Client
{
    public interface IMetricsAgentClient
    {
        IList<AllRamMetricsApiResponse> GetAllRamMetrics(GetAllRamMetricsApiRequest request);
        IList<AllHddMetricsApiResponse> GetAllHddMetrics(GetAllHddMetricsApiRequest request);
        IList<DonNetMetricsApiResponse> GetDonNetMetrics(DonNetHeapMetrisApiRequest request);
        IList<AllCpuMetricsApiResponse> GetCpuMetrics(GetAllCpuMetricsApiRequest request);
        IList<AllNetWorkMetricsApiResponse> GetNetWorkMetrics(GetAllNetWorkTrafficMetricsApiRequest request);
    }
}
