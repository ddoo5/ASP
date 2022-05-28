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



        /// <summary>
        /// Получает Hdd метрики по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/hdd/agent/1
        ///
        /// </remarks>
        /// <returns>Метрику с существующим id</returns>
        /// <param name = "agentId" > id метрики</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code = "400" > Запрос не выполнен</response>
        /// <response code = "404" > Запрос не выполнен. Страница не существует</response>
        /// <response code = "405" > Запрос не выполнен. Недостаточно прав</response>
        /// <response code = "408" > Соединение разорвано</response>
        /// <response code = "414" > URI Too Long</response>
        /// <response code = "415" > Unsupported Media Type </response>


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


        /// <summary>
        /// Получает все Hdd метрики
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/hdd/cluster
        ///
        /// </remarks>
        /// <returns>Список всех метрик</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code = "400" > Запрос не выполнен</response>
        /// <response code = "404" > Запрос не выполнен. Страница не существует</response>
        /// <response code = "405" > Запрос не выполнен. Недостаточно прав</response>
        /// <response code = "408" > Соединение разорвано</response>
        /// <response code = "414" > URI Too Long</response>
        /// <response code = "415" > Unsupported Media Type </response>


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


        /// <summary>
        /// Получает метрики Hdd на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/hdd/from/02.02.2022/to/03.03.2022
        ///
        /// </remarks>
        /// <param name = "fromTime" > начальная метрика времени в секундах с 01.01.1970</param>
        /// <param name = "toTime" > конечная метрика времени в секундах с 01.01.1970</param>
        /// <returns>Список метрик, сохранённых в заданном диапазоне времени</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code = "400" > Запрос не выполнен. Вероятно из-за неправильно переданных данных</response>
        /// <response code = "404" > Запрос не выполнен. Страница не существует</response>
        /// <response code = "405" > Запрос не выполнен. Недостаточно прав</response>
        /// <response code = "408" > Соединение разорвано</response>
        /// <response code = "414" > URI Too Long</response>
        /// <response code = "415" > Unsupported Media Type </response>


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
