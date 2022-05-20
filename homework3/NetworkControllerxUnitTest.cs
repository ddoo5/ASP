using Metrics.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using WorkWithBD;
using Xunit;


namespace xUnitForMetricsAgent
{
	public class NetworkControllerxUnitTest
	{
        private NetworkController controller;
        private Mock<INetworkMetricsRepository> mock;
        private Mock<ILogger<NetworkController>> mockLogger;


        public NetworkControllerxUnitTest()
        {
            mockLogger = new Mock<ILogger<NetworkController>>();
            mock = new Mock<INetworkMetricsRepository>();
            controller = new NetworkController(mockLogger.Object, mock.Object);
        }



        [Fact]
        public void Create_ShouldCreate_From_CpuController()
        {
            mock.Setup(repository => repository.Create(It.IsAny<NetworkMetrics>())).Verifiable();
            var result = controller.Create(new NetworkMetricCreateRequest
            {
                Time = DateTime.Now,
                Value = 50
            });
            mock.Verify(repository => repository.Create(It.IsAny<NetworkMetrics>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetAll_ShouldGetAll_From_CpuController()
        {
            var responseList = new List<NetworkMetrics>()
         {
                new NetworkMetrics(){ Id = 1,Value = 11,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 2,Value = 22,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 3,Value = 33,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 4,Value = 44,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 5,Value = 55,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 6,Value = 66,Time = DateTime.Now}
         };

            mock.Setup(repository => repository.GetAll()).Returns(responseList);
            var result = controller.GetAll();
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }


        [Fact]
        public void Delete_ShouldDeleteById_From_CpuController()
        {
            var responseList = new List<NetworkMetrics>()
            {
                new NetworkMetrics(){ Id = 1,Value = 11,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 2,Value = 22,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 3,Value = 33,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 4,Value = 44,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 5,Value = 55,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 6,Value = 66,Time = DateTime.Now}
            };

            mock.Setup(repository => repository.Delete(4));
            var result = controller.Delete(4);
            mock.Verify(repository => repository.Delete(4), Times.AtMostOnce());
        }


        [Fact]
        public void GetById_ShouldGetById_From_CpuController()
        {
            var responseList = new List<NetworkMetrics>()
            {
                new NetworkMetrics(){ Id = 1,Value = 11,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 2,Value = 22,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 3,Value = 33,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 4,Value = 44,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 5,Value = 55,Time = DateTime.Now},
                new NetworkMetrics(){ Id = 6,Value = 66,Time = DateTime.Now}
            };

            var back = new NetworkMetrics() { Id = 3, Value = 33, Time = DateTime.Now };

            mock.Setup(repository => repository.GetById(3)).Returns(back);
            var result = controller.GetById(3);
            mock.Verify(repository => repository.GetById(3), Times.AtMostOnce());
        }
    }
}

