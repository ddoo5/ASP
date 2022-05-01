using WorkWithBD;

namespace Microsoft.OpenApi.Models
{
    internal class CpuMetric : CpuMetrics
    {
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
    }
}