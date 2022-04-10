using Metrics.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;


namespace MetricsTests
{
	public class HddControllerUnitTest
	{
		private HddController controller;

		public HddControllerUnitTest()
		{
			controller = new HddController();
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
}

