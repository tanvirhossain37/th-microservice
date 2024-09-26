using System;
using System.Collections.Generic;

namespace TH.RnD.CL.Models;

public partial class Country
{
    public string Id { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Active { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string IsoCode { get; set; } = null!;

    public string CurrencyName { get; set; } = null!;

    public string CurrencyCode { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
