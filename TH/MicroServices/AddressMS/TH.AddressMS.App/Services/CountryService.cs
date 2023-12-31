using SharpCompress.Common;
using TH.AddressMS.Core;

namespace TH.AddressMS.App
{
    public class CountryService : BaseService, ICountryService
    {
        private readonly ICountryRepo _repo;

        public CountryService(ICountryRepo countryRepo)
        {
            _repo = countryRepo;
        }

        public Task<bool> DeleteAsync(Country entity)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            ;
        }

        public async Task<Country> FindByIdAsync(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await Task.Run(() => _repo.GetById(id));
            if (result is null)
                return null;

            return result;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await Task.Run(() => _repo.GetAll().ToList());
        }

        public async Task<Country> SaveAsync(Country entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await Task.Run(() => _repo.Add(entity));
            if (result)
                return entity;

            return null;
        }

        public Task<Country> UpdateAsync(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}