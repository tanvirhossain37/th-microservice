using System.Linq.Expressions;
using TH.Common.Lang;
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
        //get modules
        var modules = await Repo.ModuleRepo.GetQueryableAsync(x => x.ParentId == null, i => i.InverseParent, o => o.OrderBy(m => m.Id), (int)PageEnum.PageIndex,
            (int)PageEnum.All, dataFilter);

        foreach (var module in modules)
        {
            await AddPermissionRecursivelyAsync(entity, module, null, dataFilter);
        }
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

    private async Task ApplyOnArchivingBlAsync(Role existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivedBlAsync(Role existingEntity, DataFilter dataFilter)
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

    private async Task ApplyCustomGetFilterBlAsync(RoleFilterModel filter, List<Expression<Func<Role, bool>>> predicates, List<Expression<Func<Role, object>>> includePredicates, DataFilter dataFilter)
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

    private async Task AddPermissionRecursivelyAsync(Role role, Module module, Permission parentPermission, DataFilter dataFilter)
    {
        try
        {
            if (module == null) throw new ArgumentNullException(nameof(module));

            var permission = new Permission
            {
                Id = Util.TryGenerateGuid(),
                CreatedDate = role.CreatedDate,
                SpaceId = role.SpaceId,
                CompanyId = role.CompanyId,
                RoleId = role.Id,
                ParentId = parentPermission?.Id,
                ModuleId = module.Id,
                Read = role.Name.Equals("Super Admin") ? true : false,
                Write = role.Name.Equals("Super Admin") ? true : false,
                Update = role.Name.Equals("Super Admin") ? true : false,
                Delete = role.Name.Equals("Super Admin") ? true : false,
                MenuOrder = module.MenuOrder,
                Level = module.Level
            };

            parentPermission?.InverseParent.Add(permission);

            role.Permissions.Add(permission);
            module.Permissions.Add(permission);

            foreach (var childModule in module.InverseParent)
            {
                await AddPermissionRecursivelyAsync(role, childModule, permission, dataFilter);
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