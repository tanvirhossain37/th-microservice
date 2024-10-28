namespace TH.AddressMS.App;

public class ExcelResult
{
    public int TotalRowCount { get; set; }
    public int SuccessCount { get; set; }
    public int ErrorCount { get; set; }
    public List<ErrorDetail> ErrorDetails { get; set; } = new List<ErrorDetail>();
}