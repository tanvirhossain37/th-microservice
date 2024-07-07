namespace TH.Common.Model;

public class SortFilter
{
    public string PropertyName { get; set; }
    public OrderByEnum Operation { get; set; }

    public SortFilter()
    {
        PropertyName = null;
        Operation = OrderByEnum.Ascending;
    }
}