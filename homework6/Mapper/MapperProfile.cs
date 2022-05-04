using AutoMapper;
using MManager.Dto;
using MManager.Models;

namespace MetricsAgent.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<CpuMetric, CpuMetricDTO>();
			CreateMap<RamMetric, RamMetricDTO>();
			CreateMap<HddMetric, HddMetricDTO>();
			CreateMap<DotNetMetric, DotNetMetricDTO>();
			CreateMap<NetWorkMetric, NetWorkMetricDTO>();
		}
	}
}

