﻿using AutoMapper;
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



        /// <summary>
        /// Получает Network метрики по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/network/agent/1
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


        /// <summary>
        /// Получает все Network метрики
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/network/cluster
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


        /// <summary>
        /// Получает метрики Network на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/network/from/02.02.2022/to/03.03.2022
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
