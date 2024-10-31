using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.CompanyMS.App
{
    public class AddressViewModel
    {
        public string Id { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? State { get; set; }
        public string PostalCode { get; set; } = null!;
        public string CountryId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public bool IsDefault { get; set; }
        public string CountryName { get; set; }
        public string ClientName { get; set; }
    }
}