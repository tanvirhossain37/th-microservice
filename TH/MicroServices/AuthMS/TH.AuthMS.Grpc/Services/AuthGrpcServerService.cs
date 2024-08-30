
using AutoMapper;
using Grpc.Core;
using TH.AuthMS.App;
using TH.AuthMS.Grpc;
using TH.Common.Model;

namespace TH.AuthMS.Grpc;

public class AuthGrpcServerService : AuthProtoService.AuthProtoServiceBase, IDisposable
{
    private readonly ILogger<AuthGrpcServerService> _logger;
    private readonly IMapper _mapper;
    private IAuthService _authService;
    public DataFilter DataFilter { get; set; }

    public AuthGrpcServerService(ILogger<AuthGrpcServerService> logger, IMapper mapper, IAuthService authService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));

        DataFilter = new DataFilter
        {
            IncludeInactive = false
        };
    }

    public override async Task<ApplicationUserViewReply> FindApplicationUserByEmail(ApplicationUserFilterRequest request, ServerCallContext context)
    {
        var filter = _mapper.Map<ApplicationUserFilterRequest, ApplicationUserFilterModel>(request);

        var applicationUser = await _authService.FindByEmailAsync(filter, DataFilter);

        var viewReply = _mapper.Map<ApplicationUser, ApplicationUserViewReply>(applicationUser);

        return viewReply;
    }

    public void Dispose()
    {
        _authService?.Dispose();
    }
}