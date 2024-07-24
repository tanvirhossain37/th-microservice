using System.Collections.Generic;
using TH.Common.Model;

namespace TH.AuthMS.App;

public partial class PermissionFilterModel
{
    public string Id { get; set; } = null!;
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool? Active { get; set; }
    public string SpaceId { get; set; } = null!;
    public string CompanyId { get; set; } = null!;
    public string RoleId { get; set; } = null!;
    public string ModuleId { get; set; } = null!;
    public bool? Read { get; set; }
    public bool? Write { get; set; }
    public bool? Update { get; set; }
    public bool? Delete { get; set; }
    public int AccessTypeId { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}