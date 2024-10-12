using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.Grpc;

public class CompanyGrpcServerService : CompanyProtoService.CompanyProtoServiceBase, IDisposable
{
    private readonly ILogger<CompanyGrpcServerService> _logger;
    private readonly IMapper _mapper;
    private readonly ISpaceSubscriptionService _spaceSubscriptionService;
    public DataFilter DataFilter { get; set; }

    public CompanyGrpcServerService(ILogger<CompanyGrpcServerService> logger, IMapper mapper, ISpaceSubscriptionService spaceSubscriptionService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _spaceSubscriptionService = spaceSubscriptionService ?? throw new ArgumentNullException(nameof(spaceSubscriptionService));

        DataFilter = new DataFilter
        {
            IncludeInactive = false
        };
    }

    public override async Task<BoolValue> TrySaveSpaceSubscription(SpaceSubscriptionInputRequest request, ServerCallContext context)
    {
        try
        {
            var model = _mapper.Map<SpaceSubscriptionInputRequest, SpaceSubscription>(request);
            var entity = await _spaceSubscriptionService.SaveAsync(model, DataFilter);
            return new BoolValue { Value = true };
        }
        catch (Exception )
        {
            return new BoolValue { Value = false };
        }
    }

    public override async Task<SpaceSubscriptionViewReply> TryFindBySpaceId(SpaceSubscriptionFilterRequest request, ServerCallContext context)
    {
        try
        {
            var model = _mapper.Map<SpaceSubscriptionFilterRequest, SpaceSubscriptionFilterModel>(request);
            var entity = await _spaceSubscriptionService.FindBySpaceIdAsync(model, DataFilter);
            var viewReply = _mapper.Map<SpaceSubscription, SpaceSubscriptionViewReply>(entity);

            return viewReply;
        }
        catch (Exception )
        {
            return null;
        }
    }

    public void Dispose()
    {
        _spaceSubscriptionService?.Dispose();
    }
}