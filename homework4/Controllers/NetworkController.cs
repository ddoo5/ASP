using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkWithBD;

namespace Metrics.Controllers
{
    [Route("api/metric/network")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        private INetworkMetricsRepository repository;
        private readonly ILogger<NetworkController> _logger;
        private readonly IMapper _mapper;


        public NetworkController(ILogger<NetworkController> logger, INetworkMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog entered in NetworkController");
            this.repository = repository;
            this._mapper = mapper;
            _logger.LogDebug(1, "Mapper entered in NetworkController");
        }



        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation(1, $"Entered request: Value: {request.Value},\n Time: {request.Time}");
            repository.Create(new NetworkMetrics
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok("Created");
        }


        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation(1, $"Entered request: Display networkmetrics database");
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetrics, NetworkMetricDto>());
            var mapper = config.CreateMapper();
            IList<NetworkMetrics> metrics = repository.GetAll();
            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<NetworkMetricDto>(metric));
            }
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto { Id = metric.Id, Value = metric.Value, Time = metric.Time });
                _logger.LogInformation(2, $"Displayed: {metric.Id}, {metric.Value}, {metric.Time}");
            }
            return Ok(response);
        }


        [HttpPost("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            _logger.LogInformation(1, $"Entered request: Delete {id} item in networkmetrics database");
            repository.Delete(id);
            return Ok("Deleted");
        }


        [HttpPost("update")]
        public IActionResult Update([FromBody] NetworkMetrics metric)
        {
            _logger.LogInformation(1, $"Entered request: Update networkmetrics database");
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
            _logger.LogInformation(1, $"Entered request: Display {id} item from networkmetrics database");
            var metrics = repository.GetById(id);
            return Ok(metrics);
        }


        [HttpGet("api/metrics/network/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Id: {agentId}\n from time: {fromTime}\n to time: {toTime}");
            return Ok();
        }
    }
}

