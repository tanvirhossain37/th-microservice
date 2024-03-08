using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AuthMS.Core
{
    public interface IEntity
    {
        int UserTypeId { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        string? RefreshToken { get; set; }
        DateTime? RefreshTokenExpiryTime { get; set; }
    }
}