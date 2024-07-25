using AutoMapper;
using TH.Grpc.Protos;

namespace TH.AuthMS.App.GrpcServices;

public class CompanyGrpcClientService
{
    private readonly CompanyProtoService.CompanyProtoServiceClient _grpcClient;
    private readonly IMapper _mapper;

    public CompanyGrpcClientService(CompanyProtoService.CompanyProtoServiceClient grpClient, IMapper mapper)
    {
        _grpcClient = grpClient ?? throw new ArgumentNullException(nameof(grpClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PermissionReply> GetPermissions(string name)
    {
        return await _grpcClient.GetPermissionsAsync(new PermissionRequest { Name = name });
    }
}