using SharpCompress.Common;
using TH.AddressMS.Core;

namespace TH.AddressMS.App
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly IAddressRepo _repo;

        public AddressService(IAddressRepo addressRepo)
        {
            _repo = addressRepo;
        }

        public async Task<Address> SaveAsync(Address entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Id=Guid.NewGuid().ToString();
            entity.CreatedDate = DateTime.Now;
            entity.Active = true;

            var result = await Task.Run(() => _repo.Add(entity));
            if (result)
                return entity;

            return null;
        }

        public Task<Address> UpdateAsync(Address entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Address entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Address> FindByIdAsync(string id)
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

        public async Task<IEnumerable<Address>> GetAllByClientIdAsync(string clientId)
        {
            if (clientId is null)
            {
                throw new ArgumentNullException(nameof(clientId));
            }

            return await Task.Run(() => _repo.GetAll(c => c.ClientId == clientId).ToList());
        }

        public override void Dispose()
        {
            ;
        }
    }
}