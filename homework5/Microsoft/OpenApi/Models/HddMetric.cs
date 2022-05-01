using WorkWithBD;

namespace Microsoft.OpenApi.Models
{
    internal class HddMetric : HddMetrics
    {
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
    }
}