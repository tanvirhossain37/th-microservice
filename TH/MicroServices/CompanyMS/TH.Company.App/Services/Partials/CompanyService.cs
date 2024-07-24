using System.Linq.Expressions;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class CompanyService
{
    //Add additional services if any

    //private CompanyService(IUow repo, IBranchUserService branchUserService, IBranchService branchService, IPermissionService permissionService, IRoleService roleService, IUserRoleService userRoleService, IUserService userService) : this()
    //{
    //}

    private void ApplyOnSavingBl(Company entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //todo
            var role = new Role();
            role.Name = "Super Admin";
            role.CompanyId = entity.Id;
            role.SpaceId = entity.SpaceId;

            entity.Roles.Add(role);

            var user = new User();
            user.Name = UserResolver.FullName;
            user.CompanyId = entity.Id;
            user.SpaceId = entity.SpaceId;
            user.UserName = UserResolver.UserName;
            user.UserTypeId = (int)UserTypeEnum.TenantUser;

            entity.Users.Add(user);

            var userRole = new UserRole();
            userRole.SpaceId = entity.SpaceId;
            userRole.CompanyId=entity.Id;
            userRole.RoleId = role.Id;
            userRole.UserId = user.Id;

            entity.UserRoles.Add(userRole);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnSavedBl(Company entity, DataFilter dataFilter)
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

    private void ApplyOnUpdatingBl(Company existingEntity, DataFilter dataFilter)
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

    private void ApplyOnUpdatedBl(Company existingEntity, DataFilter dataFilter)
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

    private void ApplyOnDeletingBl(Company existingEntity, DataFilter dataFilter)
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

    private void ApplyOnDeletedBl(Company existingEntity, DataFilter dataFilter)
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

    private void ApplyOnFindByIdBl(Company entity, DataFilter dataFilter)
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

    private void ApplyOnGetBl(CompanyFilterModel filter, DataFilter dataFilter)
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

    private void ApplyCustomGetFilterBl(CompanyFilterModel filter, List<Expression<Func<Company, bool>>> predicates)
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