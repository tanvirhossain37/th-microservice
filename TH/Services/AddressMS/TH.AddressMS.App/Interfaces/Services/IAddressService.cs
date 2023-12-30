using TH.AddressMS.Core;

namespace TH.AddressMS.App
{
    public interface IAddressService : IBaseService
    {
        Task<Address> SaveAsync(Address entity);
        Task<Address> UpdateAsync(Address entity);
        Task<bool> DeleteAsync(Address entity);
        Task<Address> FindByIdAsync(string id);
        Task<IEnumerable<Address>> GetAllByClientIdAsync(string clientId);
    }
}