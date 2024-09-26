using AutoMapper;
using Grpc.Core;
using TH.AddressMS.App;
using TH.AddressMS.Core;
using TH.Common.Model;

namespace TH.AddressMS.Grpc;

public class AddressGrpcServerService:AddressProtoService.AddressProtoServiceBase
{
    private readonly ILogger<AddressGrpcServerService> _logger;
    private readonly IMapper _mapper;
    private readonly ICountryService _countryService;
    public DataFilter DataFilter { get; set; }

    public AddressGrpcServerService(ILogger<AddressGrpcServerService> logger, IMapper mapper, ICountryService countryService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));

        DataFilter = new DataFilter
        {
            IncludeInactive = false
        };
    }

    public override async Task<CountryViewReply> FindByCode(CountryFilterRequest request, ServerCallContext context)
    {
        //var filter = _mapper.Map<CountryFilterRequest, CountryFilterModel>(request);
        //var entity = await _countryService.FindByCodeAsync(filter, DataFilter);
        //return _mapper.Map<Country, CountryViewReply>(entity);
        return null;
    }

    public void Dispose()
    {
        _countryService?.Dispose();
    }
}