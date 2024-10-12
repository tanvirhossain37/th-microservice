using AutoMapper;
using TH.AuthMS.App;
using TH.CompanyMS.App;
using TH.CompanyMS.Grpc;

namespace TH.AuthMS.API;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<ApplicationUser, SignUpViewModel>().ReverseMap();
        CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        //CreateMap<SignUpRequest, SignUpInputModel>().ReverseMap();
        //CreateMap<SignUpViewModel, SignUpReply>().ReverseMap();
        //CreateMap<UserCreateEvent, SignUpInputModel>().ReverseMap();
        CreateMap<ApplicationUser, SignInViewModel>().ReverseMap();
        CreateMap<SpaceSubscriptionViewReply, SpaceSubscriptionViewModel>().ReverseMap();
    }
}