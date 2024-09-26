using AutoMapper;
using TH.AuthMS.App;

namespace TH.AuthMS.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, SignUpViewModel>().ReverseMap();
        CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        //CreateMap<SignUpRequest, SignUpInputModel>().ReverseMap();
        //CreateMap<SignUpViewModel, SignUpReply>().ReverseMap();
        //CreateMap<UserCreateEvent, SignUpInputModel>().ReverseMap();
        CreateMap<ApplicationUser, SignInViewModel>().ReverseMap();
    }
}