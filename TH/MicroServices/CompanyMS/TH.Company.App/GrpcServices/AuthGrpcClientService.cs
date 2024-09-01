using AutoMapper;
using TH.AuthMS.Grpc;

namespace TH.CompanyMS.App;

public class AuthGrpcClientService
{
    private readonly AuthProtoService.AuthProtoServiceClient _grpcClient;
    private readonly IMapper _mapper;

    public AuthGrpcClientService(AuthProtoService.AuthProtoServiceClient grpcClient, IMapper mapper)
    {
        _grpcClient = grpcClient ?? throw new ArgumentNullException(nameof(grpcClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task<SignUpViewReply> SignUpAsync(SignUpInputRequest request)
    {
        return await _grpcClient.SignUpAsync(request);
    }
}