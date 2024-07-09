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

    private void ApplyOnSavingBl(Role entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnSavedBl(Role entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnUpdatingBl(Role existingEntity, DataFilter dataFilter)
    {
        try
        {
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnUpdatedBl(Role existingEntity, DataFilter dataFilter)
    {
        try
        {
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnDeletingBl(Role existingEntity, DataFilter dataFilter)
    {
        try
        {
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnDeletedBl(Role existingEntity, DataFilter dataFilter)
    {
        try
        {
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnFindByIdBl(Role entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnGetBl(RoleFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyCustomGetFilterBl(RoleFilterModel filter, List<Expression<Func<Role, bool>>> predicates)
    {
        try
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
        catch (Exception)
        {
            throw;
        }
    }

    private void DisposeOthers()
    {
        try
        {
        }
        catch (Exception)
        {
            throw;
        }
    }
}