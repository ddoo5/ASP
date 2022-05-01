using WorkWithBD;

namespace Microsoft.OpenApi.Models
{
    internal class RamMetric : RamMetrics
    {
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
    }
}