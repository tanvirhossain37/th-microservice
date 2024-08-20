using AutoMapper;
using TH.Grpc.Protos;

namespace TH.Grpc.Services;

public class AuthGrpcServerService: SpaceProtoService.SpaceProtoServiceBase
{
    private readonly ILogger<CompanyGrpcServerService> _logger;
    private readonly IMapper _mapper;
    //private readonly IAuthService
}