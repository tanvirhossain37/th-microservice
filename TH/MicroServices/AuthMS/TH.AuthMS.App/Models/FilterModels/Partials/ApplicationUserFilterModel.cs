using System.Collections.Generic;
using TH.Common.Model;

namespace TH.AuthMS.App;

public partial class ApplicationUserFilterModel
{   
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Email { get; set; }

    public Print Print { get; set; }
    public IList<SortFilter> SortFilters { get; set; }

    public ApplicationUserFilterModel()
    {
        PageIndex = (int) PageEnum.PageIndex;
        PageSize = (int) PageEnum.PageSize;

        Print = new Print();
        SortFilters = new List<SortFilter>();
    }
}