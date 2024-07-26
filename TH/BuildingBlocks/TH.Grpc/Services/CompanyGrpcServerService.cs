using AutoMapper;
using Grpc.Core;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;
using TH.Grpc.Protos;

namespace TH.Grpc.Services;

public class CompanyGrpcServerService : SpaceProtoService.SpaceProtoServiceBase
{
    private readonly ILogger<CompanyGrpcServerService> _logger;
    private readonly IMapper _mapper;
    private readonly IPermissionService _permissionService;

    public CompanyGrpcServerService(ILogger<CompanyGrpcServerService> logger, IMapper mapper, IPermissionService permissionService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
    }

    public override async Task<ListPermissionViewReplies> GetPermissions(PermissionFilterRequest request, ServerCallContext context)
    {
        var permissionFilterModel = _mapper.Map<PermissionFilterRequest, PermissionFilterModel>(request);

        var permissions = await _permissionService.GetAsync(permissionFilterModel, new DataFilter());
        var viewModels = _mapper.Map<List<Permission>, List<PermissionViewModel>>(permissions.ToList());

        var permissionViewReplies = _mapper.Map<List<PermissionViewModel>, List<PermissionViewReply>>(viewModels);

        var listPermissionViewReplies = new ListPermissionViewReplies();
        listPermissionViewReplies.Permissions.AddRange(permissionViewReplies);

        return listPermissionViewReplies;
    }
}