using System.Text.Json;
using MetricsAgent.Api;

namespace MManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;
        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }


        public IList<AllHddMetricsApiResponse> GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromParameter = request.fromTime;
            var toParameter = request.toTime;
            var httpRequest = new HttpRequestMessage
                (
                HttpMethod.Get,
                $"{request.ClientBaseAddress}api/hddmetricsagent/getbytimeperiod/from/{fromParameter:O}/to/{toParameter:O}"
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                return JsonSerializer.DeserializeAsync<IList<AllHddMetricsApiResponse>>
                    (
                    responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                    ).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        public IList<AllRamMetricsApiResponse> GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromParameter = request.fromTime;
            var toParameter = request.toTime;
            var httpRequest = new HttpRequestMessage
                (
                HttpMethod.Get,
                $"{request.ClientBaseAddress}api/rammetricsagent/getbytimeperiod/from/{fromParameter:O}/to/{toParameter:O}"
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<IList<AllRamMetricsApiResponse>>
                    (
                    responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                    ).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        public IList<AllCpuMetricsApiResponse> GetCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromParameter = request.fromTime;
            var toParameter = request.toTime;
            var httpRequest = new HttpRequestMessage
                (
                HttpMethod.Get,
                $"{request.ClientBaseAddress}api/cpumetricsagent/getbytimeperiod/from/{fromParameter:O}/to/{toParameter:O}"
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<IList<AllCpuMetricsApiResponse>>
                    (
                    responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                    ).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        public IList<DonNetMetricsApiResponse> GetDonNetMetrics(DonNetHeapMetrisApiRequest request)
        {
            var fromParameter = request.fromTime;
            var toParameter = request.toTime;
            var httpRequest = new HttpRequestMessage
                (
                HttpMethod.Get,
                $"{request.ClientBaseAddress}api/dotnetmetricsagent/getbytimeperiod/from/{fromParameter:O}/to/{toParameter:O}"
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<IList<DonNetMetricsApiResponse>>
                    (
                    responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                    ).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }


        public IList<AllNetWorkMetricsApiResponse> GetNetWorkMetrics(GetAllNetWorkTrafficMetricsApiRequest request)
        {
            var fromParameter = request.fromTime;
            var toParameter = request.toTime;
            var httpRequest = new HttpRequestMessage
                (
                HttpMethod.Get,
                $"{request.ClientBaseAddress}api/networkmetricsagent/getbytimeperiod/from/{fromParameter:O}/to/{toParameter:O}"
                );

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<IList<AllNetWorkMetricsApiResponse>>
                    (
                    responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
                    ).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
