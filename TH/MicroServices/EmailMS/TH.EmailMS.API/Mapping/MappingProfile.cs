using AutoMapper;
using TH.EventBus.Messages;

namespace TH.EmailMS.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EmailEvent, EmailInputModel>().ReverseMap();
    }
}