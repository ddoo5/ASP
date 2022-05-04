using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MManager.Dto;
using MManager.Models;
using MManager.Repo.IMetricsRepo;

namespace MManager.Controller
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private IHddMetricsRepository _repository;
        private readonly IMapper _mapper;



        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "Entered in HddController");
        }



        [HttpGet("agent/{agentId}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            _logger.LogInformation(2, $"Took metrics by id {agentId} from hdddatabase");

            try
            {
                IList<HddMetric> metrics = _repository.GetAgentMetricById(agentId);
                List<HddMetricDTO> Metrics = new List<HddMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<HddMetricDTO>(metric));
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
                IList<HddMetric> metrics = _repository.GetMetricsFromAllCluster();
                List<HddMetricDTO> Metrics = new List<HddMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<HddMetricDTO>(metric));
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
            _logger.LogInformation(2, $"Took metrics by time period from hdddatabase: from {fromTime} to {toTime}");

            IList<HddMetric> metrics = _repository.GetMetricsByTimePeriod(fromTime, toTime);
            List<HddMetricDTO> Metrics = new List<HddMetricDTO>();

            foreach (var metric in metrics)
            {
                Metrics.Add(_mapper.Map<HddMetricDTO>(metric));
            }
            return Ok(Metrics);
        }
    }
}
