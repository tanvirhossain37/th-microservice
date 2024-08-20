using AutoMapper;
using TH.AuthMS.API.Protos;
using TH.AuthMS.App;
using TH.EventBus.Messages;

namespace TH.AuthMS.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, SignUpViewModel>().ReverseMap();
        CreateMap<SignUpRequest, SignUpInputModel>().ReverseMap();
        CreateMap<SignUpViewModel, SignUpReply>().ReverseMap();
        CreateMap<UserCreateEvent, SignUpInputModel>().ReverseMap();
    }
}