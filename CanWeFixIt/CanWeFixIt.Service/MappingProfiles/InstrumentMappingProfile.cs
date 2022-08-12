using AutoMapper;
using CanWeFixIt.Core.Models.Entities;
using CanWeFixIt.Core.Models.Dtos;

namespace CanWeFixIt.Service.MappingProfiles
{
    public class InstrumentMappingProfile : Profile
    {
        public InstrumentMappingProfile()
        {
            CreateMap<Instrument, InstrumentDto>()
                .ForMember(d => d.Active, o => o.MapFrom(s => s.IsActive));
        }
    }
}
