using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MManager.Dto;
using MManager.Models;
using MManager.Repo.IMetricsRepo;

namespace MManager.Controller
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;



        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "Entered in CpuMetricsController");
        }



        [HttpGet("agent/{agentId}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            try
            {
                _logger.LogInformation(2,$"Took metrics by id {agentId} from cpudatabase");

                IList<CpuMetric> metrics = _repository.GetAgentMetricById(agentId);
                List<CpuMetricDTO> Metrics = new List<CpuMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<CpuMetricDTO>(metric));
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
                IList<CpuMetric> metrics = _repository.GetMetricsFromAllCluster();
                List<CpuMetricDTO> Metrics = new List<CpuMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<CpuMetricDTO>(metric));
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
            try
            {
                _logger.LogInformation(2, $"Took metrics by time period from cpudatabse: from {fromTime} to {toTime}");

                IList<CpuMetric> metrics = _repository.GetMetricsByTimePeriod(fromTime, toTime);
                List<CpuMetricDTO> Metrics = new List<CpuMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<CpuMetricDTO>(metric));

                }
                return Ok(Metrics);
            }
            catch
            {
            }
            return Ok();
        }
    }
}
