using AutoMapper;
using TH.EventBus.Messages;
using TH.ShadowMS.Core;

namespace TH.ShadowMS.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ShadowEvent, Shadow>().ReverseMap();
    }
}