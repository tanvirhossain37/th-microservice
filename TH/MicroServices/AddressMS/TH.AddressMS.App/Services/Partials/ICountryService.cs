using TH.AddressMS.Core;
using TH.Common.Model;

namespace TH.AddressMS.App;

public partial interface ICountryService : IBaseService
{
    Task<ExcelResult> TryInitAsync(string path);
}