using TH.ShadowMS.Core;

namespace TH.ShadowMS.App;

public partial class ShadowFilterModel
{
    public string Id { get; set; }
    public string SpaceId { get; set; }
    public string ClientId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? Active { get; set; }
    public string UserName { get; set; }
    public ActivityNameEnum? ActivityNameEnum { get; set; }
    public string Message { get; set; }
}