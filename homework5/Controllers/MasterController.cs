using Microsoft.AspNetCore.Mvc;

namespace Metrics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly ILogger<MasterController> _logger;


        public MasterController(ILogger<MasterController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog entered in MasterController");
        }



        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo){
            _logger.LogInformation($"Registered agent\n Id: {agentInfo}");
            return Ok();
    }


    [HttpPut("enable/{agentId}")]
    public IActionResult EnableAgentById([FromRoute] int agentId) {
            _logger.LogInformation($"Enabled agent\n Id: {agentId}");
            return Ok();
    }


    [HttpPut("disable/{agentId}")]
    public IActionResult DisableAgentById([FromRoute] int agentId)
    {
           _logger.LogInformation($"Disabled agent\n Id: {agentId}");
            return Ok();
    }
    }
}

