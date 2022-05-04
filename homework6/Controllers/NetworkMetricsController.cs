using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MManager.Dto;
using MManager.Models;
using MManager.Repo.IMetricsRepo;

namespace MManager.Controller
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetWorkMetricsController : ControllerBase
    {
        private readonly ILogger<NetWorkMetricsController> _logger;
        private INetWorkMetricsRepository _repository;
        private readonly IMapper _mapper;



        public NetWorkMetricsController(ILogger<NetWorkMetricsController> logger, INetWorkMetricsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "Entered in NetworkController");
        }



        [HttpGet("agent/{agentId}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            _logger.LogInformation(2, $"Took metrics by id {agentId} from networkdatabase");
            try
            {
                IList<NetWorkMetric> metrics = _repository.GetAgentMetricById(agentId);
                List<NetWorkMetricDTO> Metrics = new List<NetWorkMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<NetWorkMetricDTO>(metric));
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
                IList<NetWorkMetric> metrics = _repository.GetMetricsFromAllCluster();
                List<NetWorkMetricDTO> Metrics = new List<NetWorkMetricDTO>();

                foreach (var metric in metrics)
                {
                    Metrics.Add(_mapper.Map<NetWorkMetricDTO>(metric));
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
            _logger.LogInformation(2, $"Took metrics by time period from networkdatabase: from {fromTime} to {toTime}");

            IList<NetWorkMetric> metrics = _repository.GetMetricsByTimePeriod(fromTime, toTime);
            List<NetWorkMetricDTO> Metrics = new List<NetWorkMetricDTO>();

            foreach (var metric in metrics)
            {
                Metrics.Add(_mapper.Map<NetWorkMetricDTO>(metric));
            }
            return Ok(Metrics);
        }
    }
}
