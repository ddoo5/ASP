namespace MManager.AgentRequest
{
    public class GetAllRamMetricsApiRequest 
    {
        public string ClientBaseAddress { get; set; }
        public DateTimeOffset fromTime { get; set; }
        public DateTimeOffset toTime { get; set; }
    }
}
