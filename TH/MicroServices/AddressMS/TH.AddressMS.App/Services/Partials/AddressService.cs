using System.Linq.Expressions;
using TH.AddressMS.Core;
using TH.Common.Model;
using TH.Common.Util;

namespace TH.AddressMS.App;

public partial class AddressService
{
    //Add additional services if any

    //private AddressService(IUow repo) : this()
    //{
    //}

    private async Task ApplyOnSavingBlAsync(Address entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnSavedBlAsync(Address entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnUpdatingBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnUpdatedBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnSoftDeletingBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnSoftDeletedBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(Address entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(AddressFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(AddressFilterModel filter, List<Expression<Func<Address, bool>>> predicates, List<Expression<Func<Address, object>>> includePredicates, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (predicates == null) throw new ArgumentNullException(nameof(predicates));

        //todo
        //additional
        if (filter.StartDate.HasValue && filter.EndDate.HasValue)
        {
            filter.StartDate = Util.TryFloorTime((DateTime)filter.StartDate);
            filter.EndDate = Util.TryCeilTime((DateTime)filter.EndDate);

            predicates.Add(t => (t.CreatedDate >= filter.StartDate) && (t.CreatedDate <= filter.EndDate));
        }
    }

    private void DisposeOthers()
    {
        //todo
    }
}