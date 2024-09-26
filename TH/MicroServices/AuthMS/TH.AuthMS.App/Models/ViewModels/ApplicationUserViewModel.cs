using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.AuthMS.App;

public partial class ApplicationUserViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Provider { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}