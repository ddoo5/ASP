using Microsoft.AspNetCore.Mvc;
using WorkWithBD;

namespace Metrics.Controllers
{
    [Route("api/metric/hdd")]
    [ApiController]
    public class HddController : ControllerBase
    {
        private IHddMetricsRepository repository;
        private readonly ILogger<HddController> _logger;


        public HddController(ILogger<HddController> logger, IHddMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog entered in HddController");
            this.repository = repository;
        }



        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation(1, $"Entered request: Value: {request.Value},\n Time: {request.Time}");
            repository.Create(new HddMetrics
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok("Created");
        }


        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation(1, $"Entered request: Display hddmetrics database");
            var metrics = repository.GetAll();
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Id = metric.Id, Value = metric.Value, Time = metric.Time });
                _logger.LogInformation(2, $"Displayed: {metric.Id}, {metric.Value}, {metric.Time}");
            }
            return Ok(response);
        }


        [HttpPost("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            _logger.LogInformation(1, $"Entered request: Delete {id} item in hddmetrics database");
            repository.Delete(id);
            return Ok("Deleted");
        }


        [HttpPost("update")]
        public IActionResult Update([FromBody] HddMetrics metric)
        {
            _logger.LogInformation(1, $"Entered request: Update hddmetrics database");
            var metrics = repository.GetAll();
            foreach (var metricq in metrics)
            {
                if (metricq.Id == metric.Id)
                {
                    _logger.LogInformation(2, $"From: {metricq.Id}  {metricq.Value}  {metricq.Time}");
                }
            }
            _logger.LogInformation(2, $"To: {metric.Id}  {metric.Value}  {metric.Time}");
            repository.Update(metric);
            return Ok("Updated");
        }


        [HttpPost("getbyid")]
        public IActionResult GetById([FromQuery] int id)
        {
            _logger.LogInformation(1, $"Entered request: Display {id} item from hddmetrics database");
            var metrics = repository.GetById(id);
            return Ok(metrics);
        }


        [HttpGet("api/metrics/hdd/left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Id: {agentId}\n from time: {fromTime}\n to time: {toTime}");
            return Ok();
        }
    }
}

