using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MManager.Dto;
using MManager.Models;
using MManager.Repo.IMetricsRepo;

namespace MManager.Controller
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;



        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "Entered in DotNetController");
        }



        [HttpGet("agent/{agentId}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            _logger.LogInformation(2, $"Took metrics by id {agentId} from dotnetdatabase");

            try
            {
                IList<DotNetMetric> metrics = _repository.GetAgentMetricById(agentId);
                List<DotNetMetricDTO> Metrics = new List<DotNetMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<DotNetMetricDTO>(metric));
                }
                return Ok(Metrics);
            }
            catch
            {
            }
            return Ok();
        }


        [HttpGet("cluster")]
        public IActionResult GetMetricsFromAllCluster()
        {
            try
            {
                IList<DotNetMetric> metrics = _repository.GetMetricsFromAllCluster();
                List<DotNetMetricDTO> Metrics = new List<DotNetMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<DotNetMetricDTO>(metric));
                }
                return Ok(Metrics);
            }
            catch
            {
            }
            return Ok();
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsByTimePeriod([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation(2, $"Took metrics by time period from dotnetdatabase: from {fromTime} to {toTime}");

            IList<DotNetMetric> metrics = _repository.GetMetricsByTimePeriod(fromTime, toTime);
            List<DotNetMetricDTO> Metrics = new List<DotNetMetricDTO>();

            foreach (var metric in metrics)
            {
                Metrics.Add(_mapper.Map<DotNetMetricDTO>(metric));
            }
            return Ok(Metrics);
        }
    }
}
