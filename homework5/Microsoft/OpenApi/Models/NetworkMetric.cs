using WorkWithBD;

namespace Microsoft.OpenApi.Models
{
    internal class NetworkMetric : NetworkMetrics
    {
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
    }
}