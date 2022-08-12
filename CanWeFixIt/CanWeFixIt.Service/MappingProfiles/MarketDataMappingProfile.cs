using AutoMapper;
using CanWeFixIt.Core.Models;
using CanWeFixIt.Core.Models.Dtos;

namespace CanWeFixIt.Service.MappingProfiles
{
    public class MarketDataMappingProfile : Profile
    {
        public MarketDataMappingProfile()
        {
            CreateMap<MarketDataWithInstrument, MarketDataDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.MarketDataId))
                .ForMember(d => d.Active, o => o.MapFrom(s => s.IsMarketDataActive));

            CreateMap<long, MarketValuationDto>()
                .ForMember(d => d.Total, o => o.MapFrom(s => s));
        }
    }
}
