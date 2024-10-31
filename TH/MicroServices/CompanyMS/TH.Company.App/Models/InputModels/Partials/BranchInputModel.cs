using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class BranchInputModel
{
    public AddressInputModel Address { get; set; } = new AddressInputModel();
}