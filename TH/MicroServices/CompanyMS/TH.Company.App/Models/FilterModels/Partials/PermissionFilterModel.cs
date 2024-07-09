using System.Collections.Generic;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial class PermissionFilterModel
{   
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Print Print { get; set; }
    public IList<SortFilter> SortFilters { get; set; }

    public PermissionFilterModel()
    {
        PageIndex = (int) PageEnum.PageIndex;
        PageSize = (int) PageEnum.PageSize;

        Print = new Print();
        SortFilters = new List<SortFilter>();
    }
}