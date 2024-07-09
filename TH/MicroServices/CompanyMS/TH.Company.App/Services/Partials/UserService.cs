using System.Linq.Expressions;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class UserService
{
    //Add additional services if any

    //private UserService(IUow repo, IBranchUserService branchUserService, IUserRoleService userRoleService) : this()
    //{
    //}

    private void ApplyOnSavingBl(User entity, DataFilter dataFilter)
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

    private void ApplyOnSavedBl(User entity, DataFilter dataFilter)
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

    private void ApplyOnUpdatingBl(User existingEntity, DataFilter dataFilter)
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

    private void ApplyOnUpdatedBl(User existingEntity, DataFilter dataFilter)
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

    private void ApplyOnDeletingBl(User existingEntity, DataFilter dataFilter)
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

    private void ApplyOnDeletedBl(User existingEntity, DataFilter dataFilter)
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

    private void ApplyOnFindByIdBl(User entity, DataFilter dataFilter)
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

    private void ApplyOnGetBl(UserFilterModel filter, DataFilter dataFilter)
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

    private void ApplyCustomGetFilterBl(UserFilterModel filter, List<Expression<Func<User, bool>>> predicates)
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