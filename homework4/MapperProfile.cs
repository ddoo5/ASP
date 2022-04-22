using AutoMapper;
using WorkWithBD;

namespace MetricsAgent
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<CpuMetrics, CpuMetricDto>();
			CreateMap<RamMetrics, RamMetricDto>();
			CreateMap<HddMetrics, HddMetricDto>();
			CreateMap<DotNetMetrics, DotNetDto>();
			CreateMap<NetworkMetrics, NetworkMetricDto>();
		}
	}
}

