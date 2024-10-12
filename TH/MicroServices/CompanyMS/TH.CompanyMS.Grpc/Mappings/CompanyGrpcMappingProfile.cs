using AutoMapper;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Grpc;

public class CompanyGrpcMappingProfile : Profile
{
    public CompanyGrpcMappingProfile()
    {
        CreateMap<SpaceSubscriptionInputRequest, SpaceSubscription>().ReverseMap();
        CreateMap<SpaceSubscriptionFilterRequest, SpaceSubscriptionFilterModel>().ReverseMap();
        CreateMap<SpaceSubscription, SpaceSubscriptionViewReply>().ReverseMap();
    }
}