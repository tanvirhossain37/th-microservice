using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class PermissionViewModel
{
    public string SpaceName { get; set; }
    public string CompanyName { get; set; }
    public string RoleName { get; set; }
    public string ModuleName { get; set; }
    public string ModuleRoute { get; set; }
    public string ParentName { get; set; }
    public string ControllerName { get; set; }
    public IList<PermissionViewModel> InverseParent { get; set; } = new List<PermissionViewModel>();
}