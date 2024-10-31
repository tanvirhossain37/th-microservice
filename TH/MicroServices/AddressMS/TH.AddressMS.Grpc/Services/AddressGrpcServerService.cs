using AutoMapper;
using Grpc.Core;
using TH.AddressMS.App;
using TH.AddressMS.Core;
using TH.Common.Model;

namespace TH.AddressMS.Grpc;

public class AddressGrpcServerService : AddressProtoService.AddressProtoServiceBase
{
    private readonly ILogger<AddressGrpcServerService> _logger;
    private readonly IMapper _mapper;
    private readonly ICountryService _countryService;
    private readonly IAddressService _addressService;
    public DataFilter DataFilter { get; set; }

    public AddressGrpcServerService(ILogger<AddressGrpcServerService> logger, IMapper mapper, ICountryService countryService, IAddressService addressService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));

        DataFilter = new DataFilter
        {
            IncludeInactive = false
        };
        _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
    }

    public override async Task<AddressViewReply> TrySaveAddress(AddressInputRequest request, ServerCallContext context)
    {
        try
        {
            Address model = _mapper.Map<AddressInputRequest, Address>(request);
            var entity = await _addressService.SaveAsync(model, DataFilter);
            var viewModel = _mapper.Map<Address, AddressViewReply>(entity);

            return viewModel;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public override async Task<AddresssListViewReply> TryGet(AddressFilterRequest request, ServerCallContext context)
    {
        try
        {
            var filter = _mapper.Map<AddressFilterRequest, AddressFilterModel>(request);
            var entities = await _addressService.GetAsync(filter, DataFilter);

            var viewListReply = new AddresssListViewReply();

            foreach (var address in entities)
            {
                viewListReply.Addresses.Add(_mapper.Map<Address, AddressViewReply>(address));
            }

            return viewListReply;
        }
        catch (Exception)
        {
            return null;
        }
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
        _addressService?.Dispose();
    }
}