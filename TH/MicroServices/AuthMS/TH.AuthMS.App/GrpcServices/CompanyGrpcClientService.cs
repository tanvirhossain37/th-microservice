using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using TH.CompanyMS.Grpc;

namespace TH.AuthMS.App.GrpcServices;

public class CompanyGrpcClientService
{
    private readonly CompanyProtoService.CompanyProtoServiceClient _grpcClient;
    private readonly IMapper _mapper;

    public CompanyGrpcClientService(CompanyProtoService.CompanyProtoServiceClient grpcClient)
    {
        _grpcClient = grpcClient ?? throw new ArgumentNullException(nameof(grpcClient));
    }

    public async Task<BoolValue> TrySaveSpaceSubscriptionAsync(SpaceSubscriptionInputRequest request)
    {
        return await _grpcClient.TrySaveSpaceSubscriptionAsync(request);
    }

    public async Task<SpaceSubscriptionViewReply> TryFindBySpaceIdAsync(SpaceSubscriptionFilterRequest request)
    {
        return await _grpcClient.TryFindBySpaceIdAsync(request);
    }
}