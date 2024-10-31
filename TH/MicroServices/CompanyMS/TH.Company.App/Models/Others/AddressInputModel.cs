using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.CompanyMS.App
{
    public class AddressInputModel
    {
        public string Id { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? State { get; set; }
        public string PostalCode { get; set; } = null!;
        public string CountryId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}