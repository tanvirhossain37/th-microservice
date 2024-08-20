using AutoMapper;
using Grpc.Core;
using TH.AuthMS.API.Protos;
using TH.AuthMS.App;
using TH.Grpc.Services;

namespace TH.AuthMS.API;

public class AuthGrpcServerService : AuthProtoService.AuthProtoServiceBase
{
    private readonly ILogger<AuthGrpcServerService> _logger;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public AuthGrpcServerService(ILogger<AuthGrpcServerService> logger, IMapper mapper, IAuthService authService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    public override async Task<SignUpReply> SaveAuthUser(SignUpRequest request, ServerCallContext context)
    {
        var viewModel = await _authService.SignUpAsync(_mapper.Map<SignUpRequest, SignUpInputModel>(request));

        return _mapper.Map<SignUpViewModel, SignUpReply>(viewModel);
    }
}