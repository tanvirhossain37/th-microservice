using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class CompanyInputModel
{
    public IList<BranchInputModel> Branches { get; set; } = new List<BranchInputModel>();
}