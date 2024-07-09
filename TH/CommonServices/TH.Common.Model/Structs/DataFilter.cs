namespace TH.Common.Model;

public struct DataFilter
{
    public bool HasPermission { get; set; }
    public bool IsAllowed { get; set; }
    public bool IncludeInactive { get; set; }
}