namespace TH.AuthMS.Core;

public class BaseEntity
{
    public string Id { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool Active { get; set; } = true;
}