using AutoMapper;
using TH.Grpc.Protos;

namespace TH.AuthMS.App.GrpcServices;

public class CompanyGrpcClientService
{
    private readonly SpaceProtoService.SpaceProtoServiceClient _grpcClient;
    private readonly IMapper _mapper;

    public CompanyGrpcClientService(SpaceProtoService.SpaceProtoServiceClient grpcClient, IMapper mapper)
    {
        _grpcClient = grpcClient ?? throw new ArgumentNullException(nameof(grpcClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ListPermissionViewReplies> GetPermissions(PermissionFilterRequest request)
    {
        return await _grpcClient.GetPermissionsAsync(request);
    }
}