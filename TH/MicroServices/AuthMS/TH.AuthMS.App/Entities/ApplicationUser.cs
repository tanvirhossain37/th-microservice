using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AuthMS.Core;

namespace TH.AuthMS.App
{
    public class ApplicationUser : IdentityUser, IEntity
    {
        public string Name { get; set; } = null!;
        public string Provider { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? ResetPasswordToken { get; set; }
        public string? ActivationCode { get; set; }
        public DateTime CodeExpiryTime { get; set; }
        public string? ReferralId { get; set; }
        public int SubscriptionId { get; set; }
    }
}