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
    }

    private async Task ApplyOnSavedBlAsync(Company entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
        var role = new Role();
        role.SpaceId = entity.SpaceId;
        role.CompanyId = entity.Id;
        role.Name = "Super Admin";

        role = await RoleService.SaveAsync(role, dataFilter);

        var user = new User();
        user.SpaceId = entity.SpaceId;
        user.CompanyId = entity.Id;
        user.Name = UserResolver.FullName;
        user.UserName = UserResolver.UserName;
        user.AccessTypeId = (int)AccessTypeEnum.TenantAccess;
        user.UserTypeId = (int)UserTypeEnum.TenantUser;

        user = await UserService.SaveAsync(user, dataFilter);

        var userRole = new UserRole();
        userRole.SpaceId = entity.SpaceId;
        userRole.CompanyId = entity.Id;
        userRole.RoleId = role.Id;
        userRole.UserId = user.Id;

        userRole = await UserRoleService.SaveAsync(userRole, dataFilter);

        //get modules
        var modules = await Repo.ModuleRepo.GetQueryableAsync(x => x.ParentId == null, i => i.InverseParent, o => o.OrderBy(m => m.Id), (int)PageEnum.PageIndex,
            (int)PageEnum.All, dataFilter);

        foreach (var module in modules)
        {
            await AddPermissionRecursivelyAsync(entity, role, module, null, dataFilter);
        }
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

    private async Task ApplyCustomGetFilterBlAsync(CompanyFilterModel filter, List<Expression<Func<Company, bool>>> predicates, List<Expression<Func<Company, object>>> includePredicates, DataFilter dataFilter)
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

    private async Task AddPermissionRecursivelyAsync(Company company, Role role, Module module, Permission parentPermission, DataFilter dataFilter)
    {
        try
        {
            if (module == null) throw new ArgumentNullException(nameof(module));

            var permission = new Permission
            {
                SpaceId = role.SpaceId,
                CompanyId = company.Id,
                RoleId = role.Id,
                ParentId = parentPermission?.Id,
                ModuleId = module.Id,
                Read = true,
                Write = true,
                Update = true,
                Delete = true,
                MenuOrder = module.MenuOrder
            };

            permission = await PermissionService.SaveAsync(permission, dataFilter);

            foreach (var childModule in module.InverseParent)
            {
                await AddPermissionRecursivelyAsync(company, role, childModule, permission, dataFilter);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void DisposeOthers()
    {
        //todo
    }
}