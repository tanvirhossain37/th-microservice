using AutoMapper;
using TH.AuthMS.App;

namespace TH.AuthMS.Grpc;

public class AuthGrpcMappingProfile : Profile
{
    public AuthGrpcMappingProfile()
    {
        CreateMap<ApplicationUserFilterRequest, ApplicationUserFilterModel>().ReverseMap();
        CreateMap<ApplicationUser, ApplicationUserViewReply>().ReverseMap();
    }
}