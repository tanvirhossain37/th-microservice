namespace TH.Common.Model;

public class UserResolver
{
    public string SpaceId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string SpaceSubscriptionId { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool Active { get; set; }
    public int PlanId { get; set; }
    public bool IsCurrent { get; set; }
    public string? CardHolderName { get; set; }
    public string? CardNumber { get; set; }
    public string? SecurityCode { get; set; }
    public DateTime? CardExpiryDate { get; set; }
    public string? CountryId { get; set; }
    public string? ZipCode { get; set; }
}