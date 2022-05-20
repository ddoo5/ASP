using Metrics.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using WorkWithBD;
using Xunit;


namespace xUnitForMetricsAgent
{
	public class HddControllerxUnitTest
	{
        private HddController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<HddController>> mockLogger;


        public HddControllerxUnitTest()
        {
            mockLogger = new Mock<ILogger<HddController>>();
            mock = new Mock<IHddMetricsRepository>();
            controller = new HddController(mockLogger.Object, mock.Object);
        }



        [Fact]
        public void Create_ShouldCreate_From_CpuController()
        {
            mock.Setup(repository => repository.Create(It.IsAny<HddMetrics>())).Verifiable();
            var result = controller.Create(new HddMetricCreateRequest
            {
                Time = DateTime.Now,
                Value = 50
            });
            mock.Verify(repository => repository.Create(It.IsAny<HddMetrics>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetAll_ShouldGetAll_From_CpuController()
        {
            var responseList = new List<HddMetrics>()
         {
                new HddMetrics(){ Id = 1,Value = 11,Time = DateTime.Now},
                new HddMetrics(){ Id = 2,Value = 22,Time = DateTime.Now},
                new HddMetrics(){ Id = 3,Value = 33,Time = DateTime.Now},
                new HddMetrics(){ Id = 4,Value = 44,Time = DateTime.Now},
                new HddMetrics(){ Id = 5,Value = 55,Time = DateTime.Now},
                new HddMetrics(){ Id = 6,Value = 66,Time = DateTime.Now}
         };

            mock.Setup(repository => repository.GetAll()).Returns(responseList);
            var result = controller.GetAll();
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }


        [Fact]
        public void Delete_ShouldDeleteById_From_CpuController()
        {
            var responseList = new List<HddMetrics>()
            {
                new HddMetrics(){ Id = 1,Value = 11,Time = DateTime.Now},
                new HddMetrics(){ Id = 2,Value = 22,Time = DateTime.Now},
                new HddMetrics(){ Id = 3,Value = 33,Time = DateTime.Now},
                new HddMetrics(){ Id = 4,Value = 44,Time = DateTime.Now},
                new HddMetrics(){ Id = 5,Value = 55,Time = DateTime.Now},
                new HddMetrics(){ Id = 6,Value = 66,Time = DateTime.Now}
            };

            mock.Setup(repository => repository.Delete(4));
            var result = controller.Delete(4);
            mock.Verify(repository => repository.Delete(4), Times.AtMostOnce());
        }


        [Fact]
        public void GetById_ShouldGetById_From_CpuController()
        {
            var responseList = new List<HddMetrics>()
            {
                new HddMetrics(){ Id = 1,Value = 11,Time = DateTime.Now},
                new HddMetrics(){ Id = 2,Value = 22,Time = DateTime.Now},
                new HddMetrics(){ Id = 3,Value = 33,Time = DateTime.Now},
                new HddMetrics(){ Id = 4,Value = 44,Time = DateTime.Now},
                new HddMetrics(){ Id = 5,Value = 55,Time = DateTime.Now},
                new HddMetrics(){ Id = 6,Value = 66,Time = DateTime.Now}
            };

            var back = new HddMetrics() { Id = 3, Value = 33, Time = DateTime.Now };

            mock.Setup(repository => repository.GetById(3)).Returns(back);
            var result = controller.GetById(3);
            mock.Verify(repository => repository.GetById(3), Times.AtMostOnce());
        }
    }
}

