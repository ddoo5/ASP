namespace MManager.Dto
{
    public class HddMetricDTO
    {
        public long Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
        public long agentId { get; set; }
    }
}
