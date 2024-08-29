using System.Collections.Generic;
using TH.Common.Model;

namespace TH.AuthMS.App;

public partial class ApplicationUserFilterModel
{   
	public string Name { get; set; } = null!;
	public string? CompanyId { get; set; }
	public DateTime? CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public string? RefreshToken { get; set; }
	public DateTime? RefreshTokenExpiryTime { get; set; }
	public string? ResetPasswordToken { get; set; }
	public string? ActivationCode { get; set; }
	public DateTime? CodeExpiryTime { get; set; }
	public string? ReferralId { get; set; }
}