using TH.Common.Model;

namespace TH.ShadowMS.App;

public partial class ShadowFilterModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Print Print { get; set; }
    public IList<SortFilter> SortFilters { get; set; }

    public ShadowFilterModel()
    {
        PageIndex = (int)PageEnum.PageIndex;
        PageSize = (int)PageEnum.PageSize;

        Print = new Print();
        SortFilters = new List<SortFilter>();
    }
}