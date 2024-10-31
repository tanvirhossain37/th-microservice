using AutoMapper;
using TH.AddressMS.App;
using TH.AddressMS.Core;

namespace TH.AddressMS.Grpc;

public class AddressGrpcMappingProfile : Profile
{
    public AddressGrpcMappingProfile()
    {
        CreateMap<AddressInputRequest, Address>().ReverseMap();
        CreateMap<Address, AddressViewReply>().ReverseMap();
        CreateMap<AddressFilterRequest, AddressFilterModel>().ReverseMap();
        CreateMap<CountryFilterRequest, CountryFilterModel>().ReverseMap();
        CreateMap<Country, CountryViewReply>().ReverseMap();
    }
}