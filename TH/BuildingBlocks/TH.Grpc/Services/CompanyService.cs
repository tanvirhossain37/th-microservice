using Grpc.Core;
using TH.Grpc.Protos;

namespace TH.Grpc.Services;

public class CompanyService : CompanyProtoService.CompanyProtoServiceBase
{
    private readonly ILogger<CompanyService> _logger;

    public CompanyService(ILogger<CompanyService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override Task<PermissionReply> GetPermissions(PermissionRequest request, ServerCallContext context)
    {
        return Task.FromResult(new PermissionReply
        {
            Message = "Hello SPace, Mr. " + request.Name
        });
    }
}