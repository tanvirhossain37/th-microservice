using System.Collections.Generic;
using TH.Common.Model;
using TH.MongoRnDMS.App;
namespace TH.Company.App;

public partial class ModuleFilterModel
{   
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Print Print { get; set; }
    public IList<SortFilter> SortFilters { get; set; }

    public ModuleFilterModel()
    {
        PageIndex = (int) PageEnum.PageIndex;
        PageSize = (int) PageEnum.PageSize;

        Print = new Print();
        SortFilters = new List<SortFilter>();
    }
}