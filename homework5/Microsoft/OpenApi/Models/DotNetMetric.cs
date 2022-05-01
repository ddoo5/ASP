using WorkWithBD;

namespace Microsoft.OpenApi.Models
{
    internal class DotNetMetric : DotNetMetrics
    {
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
    }
}