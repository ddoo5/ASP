using Microsoft.AspNetCore.Mvc;
using MManager.Agent;
using MManager.Repo;

namespace MManager.Controller
{
    [Route("api/[controller]")]
    [ApiController]   
    public class AgentsController : ControllerBase
    {
        IAgentRepository _repository;

        public AgentsController(IAgentRepository repository)
        {
            _repository = repository;
        }



     /// <summary>
    /// Добавление агента
    /// </summary>
    /// <remarks>
   /// Пример запроса:
    ///
    /// Post api/Agents/agentregister
    ///
    /// [Body]: {
    ///                  "agentId": 0,
    ///                  "agentAddress": "something here"
    ///                    }
    ///
    /// </remarks>
    /// <param name = "agentInfo" > Тело нового агента</param>
    /// <returns>Nothing</returns>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code = "400" > Запрос не выполнен. Вероятно из-за неправильно переданных данных</response>
    /// <response code = "404" > Запрос не выполнен. Страница не существует</response>
    /// <response code = "405" > Запрос не выполнен. Недостаточно прав</response>
    /// <response code = "408" > Соединение разорвано</response>
    /// <response code = "414" > URI Too Long</response>
    /// <response code = "415" > Unsupported Media Type </response>


    [HttpPost("agentregister")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            try
            {
                _repository.SetNewAgent(new AgentModel() { AgentUrl = agentInfo.AgentAddress });
            }
            catch(Exception ex)
            {
                return Ok("Exception: " + ex.Message);
            }
            return Ok();
        }


        /// <summary>
        /// Don't work
        /// </summary>


        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }


        /// <summary>
        /// Don't work
        /// </summary>


        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }


        /// <summary>
        /// Получает всеx зарегестрированных агентов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/Agents/getagents
        ///
        /// </remarks>
        /// <returns>Список всех агентов</returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code = "400" > Запрос не выполнен</response>
        /// <response code = "404" > Запрос не выполнен. Страница не существует</response>
        /// <response code = "405" > Запрос не выполнен. Недостаточно прав</response>
        /// <response code = "408" > Соединение разорвано</response>
        /// <response code = "414" > URI Too Long</response>
        /// <response code = "415" > Unsupported Media Type </response>


        [HttpGet("getagents")]
        public IActionResult GetRegisteredObjectsList()
        {
            var agents = _repository.GetAllAgents();
            return Ok(agents);
        }
    }
}

