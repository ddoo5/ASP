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


        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }


        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }


        [HttpGet("getagents")]
        public IActionResult GetRegisteredObjectsList()
        {
            var agents = _repository.GetAllAgents();
            return Ok(agents);
        }
    }
}

