using AutoMapper;
using Microsoft.Extensions.Logging;
using TH.AuthMS.API.Protos;

namespace TH.CompanyMS.App;

public class AuthGrpcClientService : AuthProtoService.AuthProtoServiceClient
{
    private readonly AuthProtoService.AuthProtoServiceClient _grpcClient;
    private readonly IMapper _mapper;

    public AuthGrpcClientService(AuthProtoService.AuthProtoServiceClient grpcClient, IMapper mapper)
    {
        _grpcClient = grpcClient ?? throw new ArgumentNullException(nameof(grpcClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<SignUpReply> GetPermissionsAsync(SignUpRequest request)
    {
        return await _grpcClient.SaveAuthUserAsync(request);
    }
}