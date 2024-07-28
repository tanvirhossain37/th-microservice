using System.Linq.Expressions;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class RoleService
{
    //Add additional services if any

    //private RoleService(IUow repo, IPermissionService permissionService, IUserRoleService userRoleService) : this()
    //{
    //}

    private async Task ApplyOnSavingBlAsync(Role entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnSavedBlAsync(Role entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnUpdatingBlAsync(Role existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnUpdatedBlAsync(Role existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnSoftDeletingBlAsync(Role existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnSoftDeletedBlAsync(Role existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(Role existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(Role existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(Role entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(RoleFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(RoleFilterModel filter, List<Expression<Func<Role, bool>>> predicates, DataFilter dataFilter)
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