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

    private async Task ApplyOnSavingBlAsync(Company entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
        var role = new Role();
        role.Name = "Super Admin";
        role.CompanyId = entity.Id;
        role.SpaceId = UserResolver.SpaceId;

        entity.Roles.Add(role);

        var user = new User();
        user.Name = UserResolver.FullName;
        user.CompanyId = entity.Id;
        user.SpaceId = UserResolver.SpaceId;
        user.UserName = UserResolver.UserName;
        user.UserTypeId = (int)UserTypeEnum.TenantUser;

        entity.Users.Add(user);

        var userRole = new UserRole();
        userRole.SpaceId = UserResolver.SpaceId;
        userRole.CompanyId = entity.Id;
        userRole.RoleId = role.Id;
        userRole.UserId = user.Id;

        entity.UserRoles.Add(userRole);

        //permissions
        var modules = await Repo.ModuleRepo.GetQueryableAsync(null, null, o => o.OrderBy(m => m.MenuOrder), (int)PageEnum.PageIndex,
            (int)PageEnum.PageSize);
        //modules
        foreach (var module in modules)
        {
            await AddPermissionRecursivelyAsync(role, module, null, dataFilter, entity.Id);
        }
    }

    private async Task ApplyOnSavedBlAsync(Company entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnUpdatingBlAsync(Company existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnUpdatedBlAsync(Company existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnSoftDeletingBlAsync(Company existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnSoftDeletedBlAsync(Company existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(Company existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(Company existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(Company entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(CompanyFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(CompanyFilterModel filter, List<Expression<Func<Company, bool>>> predicates, DataFilter dataFilter)
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

    private async Task AddPermissionRecursivelyAsync(Role role, Module module, Permission parentPermission, DataFilter dataFilter, string companyId)
    {
        if (module == null) throw new ArgumentNullException(nameof(module));

        //disable filters as it runs in init
        var existingPermission = await Repo.PermissionRepo.SingleOrDefaultQueryableAsync(x => x.RoleId == role.Id && x.ModuleId == module.Id, dataFilter);

        //jodi pai, skip - otherwise add
        if (existingPermission == null)
        {
            var permission = new Permission
            {
                SpaceId = UserResolver.SpaceId,
                CompanyId = companyId,
                RoleId = role.Id,
                ParentId = parentPermission?.Id,
                ModuleId = module.Id,
                CreatedDate = role.CreatedDate
            };

            parentPermission = await PermissionService.SaveAsync(permission, dataFilter, false);
        }
        else
        {
            parentPermission = existingPermission;
        }

        foreach (var childModule in module.InverseParent)
        {
            await AddPermissionRecursivelyAsync(role, childModule, parentPermission, dataFilter, companyId);
        }
    }


    private void DisposeOthers()
    {
        //todo
    }
}