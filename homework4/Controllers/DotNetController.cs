using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkWithBD;

namespace Metrics.Controllers
{
    [Route("api/metric/dotnet")]
    [ApiController]
    public class DotNetController : ControllerBase
    {
        private IDotNetMetricsRepository repository;
        private readonly ILogger<DotNetController> _logger;
        private readonly IMapper _mapper;


        public DotNetController(ILogger<DotNetController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog entered in DotNetController");
            this.repository = repository;
            this._mapper = mapper;
            _logger.LogDebug(1, "Mapper entered in DotNetController");
        }



        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _logger.LogInformation(1, $"Entered request: Value: {request.Value},\n Time: {request.Time}");
            repository.Create(new DotNetMetrics
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok("Created");
        }


        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation(1, $"Entered request: Display dotnetmetrics database");
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DotNetMetrics, DotNetDto>());
            var mapper = config.CreateMapper();
            IList<DotNetMetrics> metrics = repository.GetAll();
            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<DotNetDto>(metric));
            }
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetDto { Id = metric.Id, Value = metric.Value, Time = metric.Time });
                _logger.LogInformation(2, $"Displayed: {metric.Id}, {metric.Value}, {metric.Time}");
            }
            return Ok(response);
        }


        [HttpPost("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            _logger.LogInformation(1, $"Entered request: Delete {id} item in dotnetmetrics database");
            repository.Delete(id);
            return Ok("Deleted");
        }


        [HttpPost("update")]
        public IActionResult Update([FromBody] DotNetMetrics metric)
        {
            _logger.LogInformation(1, $"Entered request: Update dotnetmetrics database");
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
            _logger.LogInformation(1, $"Entered request: Display {id} item from dotnetmetrics database");
            var metrics = repository.GetById(id);
            return Ok(metrics);
        }


        [HttpGet("api/metrics/dotnet/errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Id: {agentId}\n from time: {fromTime}\n to time: {toTime}");
            return Ok();
        }
    }
}

