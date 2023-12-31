using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.PhoneMS.Core
{
    public class Phone : BaseEntity
    {
        public string CountryCode { get; set; } = null!;
        public string Number { get; set; } = null!;
        public bool IsDefault { get; set; }
        public string ClientId { get; set; } = null!;
    }
}