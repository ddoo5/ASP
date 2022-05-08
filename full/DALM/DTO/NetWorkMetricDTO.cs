namespace MManager.Dto
{
    public class NetWorkMetricDTO
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
        public long agentId { get; set; }
    }
}
