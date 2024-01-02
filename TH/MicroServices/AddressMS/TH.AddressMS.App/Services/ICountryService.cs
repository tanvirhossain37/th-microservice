using TH.AddressMS.Core;
using TH.AddressMS.Core;

namespace TH.AddressMS.App
{
    public interface ICountryService : IBaseService
    {
        Task<Country> SaveAsync(Country entity);
        Task<Country> UpdateAsync(Country entity);
        Task<bool> DeleteAsync(Country entity);
        Task<Country> FindByIdAsync(string id);
        Task<IEnumerable<Country>> GetAllAsync();
    }
}