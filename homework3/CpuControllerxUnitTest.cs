using Metrics.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using WorkWithBD;
using Xunit;


namespace xUnitForMetricsAgent;

public class CpuControllerxUnitTest
{
    private CpuController controller;
    private Mock<ICpuMetricsRepository> mock;
    private Mock<ILogger<CpuController>> mockLogger;


    public CpuControllerxUnitTest()
    {
        mockLogger = new Mock<ILogger<CpuController>>();
        mock = new Mock<ICpuMetricsRepository>();
        controller = new CpuController(mockLogger.Object, mock.Object);
    }



    [Fact]
    public void Create_ShouldCreate_From_CpuController()
    {
        mock.Setup(repository => repository.Create(It.IsAny<CpuMetrics>())).Verifiable();
        var result = controller.Create(new CpuMetricCreateRequest {
            Time = DateTime.Now, Value = 50
        });
        mock.Verify(repository => repository.Create(It.IsAny<CpuMetrics>()), Times.AtMostOnce());
    }


    [Fact]
    public void GetAll_ShouldGetAll_From_CpuController()
    {
        var responseList = new List<CpuMetrics>()
         {
                new CpuMetrics(){ Id = 1,Value = 11,Time = DateTime.Now},
                new CpuMetrics(){ Id = 2,Value = 22,Time = DateTime.Now},
                new CpuMetrics(){ Id = 3,Value = 33,Time = DateTime.Now},
                new CpuMetrics(){ Id = 4,Value = 44,Time = DateTime.Now},
                new CpuMetrics(){ Id = 5,Value = 55,Time = DateTime.Now},
                new CpuMetrics(){ Id = 6,Value = 66,Time = DateTime.Now}
         };

        mock.Setup(repository => repository.GetAll()).Returns(responseList);
        var result = controller.GetAll();
        mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
    }


    [Fact]
    public void Delete_ShouldDeleteById_From_CpuController()
    {
        var responseList = new List<CpuMetrics>()
            {
                new CpuMetrics(){ Id = 1,Value = 11,Time = DateTime.Now},
                new CpuMetrics(){ Id = 2,Value = 22,Time = DateTime.Now},
                new CpuMetrics(){ Id = 3,Value = 33,Time = DateTime.Now},
                new CpuMetrics(){ Id = 4,Value = 44,Time = DateTime.Now},
                new CpuMetrics(){ Id = 5,Value = 55,Time = DateTime.Now},
                new CpuMetrics(){ Id = 6,Value = 66,Time = DateTime.Now}
            };

        mock.Setup(repository => repository.Delete(4));
        var result = controller.Delete(4);
        mock.Verify(repository => repository.Delete(4), Times.AtMostOnce());
    }


    [Fact]
    public void GetById_ShouldGetById_From_CpuController()
    {
        var responseList = new List<CpuMetrics>()
            {
                new CpuMetrics(){ Id = 1,Value = 11,Time = DateTime.Now},
                new CpuMetrics(){ Id = 2,Value = 22,Time = DateTime.Now},
                new CpuMetrics(){ Id = 3,Value = 33,Time = DateTime.Now},
                new CpuMetrics(){ Id = 4,Value = 44,Time = DateTime.Now},
                new CpuMetrics(){ Id = 5,Value = 55,Time = DateTime.Now},
                new CpuMetrics(){ Id = 6,Value = 66,Time = DateTime.Now}
            };

        var back = new CpuMetrics() { Id = 3, Value = 33, Time = DateTime.Now };

        mock.Setup(repository => repository.GetById(3)).Returns(back);
        var result = controller.GetById(3);
        mock.Verify(repository => repository.GetById(3), Times.AtMostOnce());
    }
}
