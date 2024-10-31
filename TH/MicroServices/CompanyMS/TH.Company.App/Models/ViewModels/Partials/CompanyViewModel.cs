using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class CompanyViewModel
{
    public string SpaceName { get; set; }
    public IList<BranchViewModel> Branches { get; set; } = new List<BranchViewModel>();
}