using AutoMapper;
using TH.AddressMS.App;
using TH.AddressMS.Core;
using TH.Common.Lang;

namespace TH.AddressMS.API;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<Address, AddressFilterModel>().ReverseMap();
        CreateMap<Country, CountryFilterModel>().ReverseMap();

        CreateMap<AddressInputModel, Address>().ReverseMap();
        CreateMap<CountryInputModel, Country>().ReverseMap();
        CreateMap<AddressViewModel, Address>().ReverseMap()
            .ForMember(dest => dest.CountryName, m => m.MapFrom(src => GetCountryName(src)));

        CreateMap<AddressViewModel, Address>().ReverseMap();
        CreateMap<CountryViewModel, Country>().ReverseMap()
            .ForMember(dest => dest.Name, m => m.MapFrom(src => src.Name))
            .ForMember(dest => dest.CurrencyName, m => m.MapFrom(src => src.CurrencyName));
    }

    private string GetCountryName(Address src)
    {
        return src.Country.Name;
    }
}