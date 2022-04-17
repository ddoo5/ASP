using Microsoft.AspNetCore.Mvc;
using WorkWithBD;

namespace Metrics.Controllers
{
    [Route("api/metric/cpu")]
    [ApiController]
    public class CpuController : ControllerBase
    {
        private ICpuMetricsRepository repository;
        private readonly ILogger<CpuController> _logger;


        public CpuController(ILogger<CpuController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog entered in CpuController");
            this.repository = repository;
        }



        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation(1, $"Entered request: Value: {request.Value},\n Time: {request.Time}");
            repository.Create(new CpuMetrics
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok("Created");
        }


        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation(1,$"Entered request: Display cpumetrics database");
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
            _logger.LogInformation(1, $"Entered request: Delete {id} item in cpumetrics database");
             repository.Delete(id);
            return Ok("Deleted");
        }


        [HttpPost("update")]
        public IActionResult Update([FromBody] CpuMetrics metric)
        {
            _logger.LogInformation(1, $"Entered request: Update cpumetrics database");
            var metrics = repository.GetAll();
            foreach (var metricq in metrics)
            {
                if(metricq.Id == metric.Id)
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
            _logger.LogInformation(1, $"Entered request: Display {id} item from cpumetrics database");
            var metrics = repository.GetById(id);
            return Ok(metrics);
        }


        [HttpGet("api/metrics/cpu/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation(0, $"Id: {agentId}\n from time: {fromTime}\n to time: {toTime}");
            return Ok();
        }
    }
}

