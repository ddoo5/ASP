using Metrics.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsTest;

public class CpuControllerUnitTest
{
    private CpuController controller;

    public CpuControllerUnitTest()
    {
        controller = new CpuController();
    }


    [Fact]
    public void GetMetricsFromAgent_ReturnsOk()
    {
        int agentId = 1;

        TimeSpan fromTime = TimeSpan.FromSeconds(0);
        TimeSpan toTime = TimeSpan.FromSeconds(100);

        var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
