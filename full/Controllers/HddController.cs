using AutoMapper;
using Microsoft.OpenApi.Models;
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
        private readonly IMapper _mapper;


        public HddController(ILogger<HddController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog entered in HddController");
            this.repository = repository;
            this._mapper = mapper;
            _logger.LogDebug(1, "Mapper entered in HddController");
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetrics, HddMetricDto>());
            var mapper = config.CreateMapper();
            IList<HddMetrics> metrics = repository.GetAll();
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<HddMetricDto>(metric));
            }
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto { Id = metric.Id, Value = metric.Value, Time = metric.Time });
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


        [HttpPost("api/metric/hdd/from/{fromTime}/to/{toTime}")]
        public IActionResult GetById([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            IList<HddMetrics> metrics = repository.GetMetricsByTimePeriod(fromTime, toTime);
            List<HddMetricDto> Metrics = new List<HddMetricDto>();

            foreach (var metric in metrics)
            {
                Metrics.Add(_mapper.Map<HddMetricDto>(metric));

            }
            return Ok(Metrics);
        }


        [HttpGet("api/metric/hdd/getbyid")]
        public IActionResult GetMetricsFromAgent([FromQuery] int id)
        {
            _logger.LogInformation(1, $"Entered request from client: display {id} item from hddmetrics database");
            var metrics = repository.GetById(id);
            return Ok(metrics);
        }
    }
}

