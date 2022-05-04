using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MManager.Dto;
using MManager.Models;
using MManager.Repo.IMetricsRepo;

namespace MManager.Controller
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {

        private readonly ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository _repository;
        private readonly IMapper _mapper;



        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "Entered in RamController");
        }



        [HttpGet("agent/{agentId}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            _logger.LogInformation(2, $"Took metrics by id {agentId} from ramdatabase");
            try
            {
                IList<RamMetric> metrics = _repository.GetAgentMetricById(agentId);
                List<RamMetricDTO> Metrics = new List<RamMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<RamMetricDTO>(metric));
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
                IList<RamMetric> metrics = _repository.GetMetricsFromAllCluster();
                List<RamMetricDTO> Metrics = new List<RamMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<RamMetricDTO>(metric));
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
            _logger.LogInformation(2, $"Took metrics by time period from ramdatabase: from {fromTime} to {toTime}");

            IList<RamMetric> metrics = _repository.GetMetricsByTimePeriod(fromTime, toTime);
            List<RamMetricDTO> Metrics = new List<RamMetricDTO>();

            foreach (var metric in metrics)
            {
                Metrics.Add(_mapper.Map<RamMetricDTO>(metric));
            }
            return Ok(Metrics);
        }
    }
}
