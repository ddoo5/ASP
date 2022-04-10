using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace Metrics.Controllers
{
    [Route("api/metric/cpu")]
    [ApiController]
    public class CpuController : ControllerBase
    {
        [HttpGet("api/metrics/cpu/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}

