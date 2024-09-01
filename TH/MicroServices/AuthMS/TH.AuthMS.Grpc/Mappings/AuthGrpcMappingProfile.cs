using AutoMapper;
using TH.AuthMS.App;

namespace TH.AuthMS.Grpc;

public class AuthGrpcMappingProfile : Profile
{
    public AuthGrpcMappingProfile()
    {
        CreateMap<SignUpInputRequest, SignUpInputModel>().ReverseMap();
        CreateMap<SignUpViewModel, SignUpViewReply>().ReverseMap();
        CreateMap<ApplicationUser, SignUpViewModel>().ReverseMap();
        CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
    }
}