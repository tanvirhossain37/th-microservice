using System.Linq.Expressions;
using TH.Common.Lang;
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

        //branch
        if (entity.Branches.Count <= 0) throw new CustomException($"{Lang.Find("validation_error")}: Branches");

        foreach (var branch in entity.Branches)
        {
            branch.Id = Util.TryGenerateGuid();
            branch.CompanyId = entity.Id;
            branch.SpaceId = entity.SpaceId;
        }

        //todo
        var role = new Role();
        role.Id = Util.TryGenerateGuid();
        role.CreatedDate = entity.CreatedDate;
        role.SpaceId = entity.SpaceId;
        role.CompanyId = entity.Id;
        role.Name = Config.GetSection("RoleName").Value?.Trim();

        entity.Roles.Add(role);

        var user = new User();
        user.Id = Util.TryGenerateGuid();
        user.CreatedDate=entity.CreatedDate;
        user.SpaceId = entity.SpaceId;
        user.CompanyId = entity.Id;
        user.Name = UserResolver.Name;
        user.UserName = UserResolver.UserName;
        user.AccessTypeId = (int)AccessTypeEnum.TenantAccess;
        user.UserTypeId = (int)UserTypeEnum.TenantUser;

        entity.Users.Add(user);

        var userCompany = new UserCompany
        {
            Id = Util.TryGenerateGuid(),
            SpaceId = entity.SpaceId,
            CompanyId = entity.Id,
            TypeId = (int)CompanyTypeEnum.Owner,
            StatusId = (int)InvitationStatusEnum.Accept,
            UserId = user.Id
        };

        user.UserCompanies.Add(userCompany);
        entity.UserCompanies.Add(userCompany);

        var userRole = new UserRole();
        userRole.Id = Util.TryGenerateGuid();
        userRole.CreatedDate = entity.CreatedDate;
        userRole.SpaceId = entity.SpaceId;
        userRole.CompanyId = entity.Id;
        userRole.RoleId = role.Id;
        userRole.UserId = user.Id;

        role.UserRoles.Add(userRole);
        user.UserRoles.Add(userRole);
        entity.UserRoles.Add(userRole);
    }

    private async Task ApplyOnSavedBlAsync(Company entity, DataFilter dataFilter)
    {
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
                Id = Util.TryGenerateGuid(),
                CreatedDate = company.CreatedDate,
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

            if (parentPermission == null)
            {
                company.Permissions.Add(permission);
            }
            else
            {
                parentPermission.InverseParent.Add(permission);
            }

            //permission = await PermissionService.SaveAsync(permission, dataFilter);

            role.Permissions.Add(permission);
            module.Permissions.Add(permission);

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