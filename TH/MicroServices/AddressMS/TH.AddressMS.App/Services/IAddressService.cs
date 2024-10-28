using TH.AddressMS.Core;
using TH.Common.Model;

namespace TH.AddressMS.App;

public partial interface IAddressService : IBaseService
{
    Task<Address> SaveAsync(Address entity, DataFilter dataFilter, bool commit = true);
    Task<Address> UpdateAsync(Address entity, DataFilter dataFilter, bool commit = true);
    Task<bool> ArchiveAsync(Address entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(Address entity, DataFilter dataFilter, bool commit = true);
    Task<Address> FindByIdAsync(AddressFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Address>> GetAsync(AddressFilterModel filter, DataFilter dataFilter);
}