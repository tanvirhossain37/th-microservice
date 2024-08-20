using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class UserInputModel
{
    public string Password { get; set; }
}