using System.Linq.Expressions;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class ModuleService
{
    //Add additional services if any

    //private ModuleService(IUow repo, IPermissionService permissionService) : this()
    //{
    //}

    public async Task InitAsync(DataFilter dataFilter)
    {
        var companyDashboard = await SaveAsync(new Module
        {
            ParentId = null,
            Name = "m_company_dashboard",
            ControllerName = "Company",
            Route = "company/dashboard",
            Icon = "group",
            MenuOrder = 1,
        }, dataFilter);

        var admin = await SaveAsync(new Module
        {
            ParentId = null,
            Name = "m_company_admin",
            ControllerName = "",
            Route = "company/admin",
            Icon = "group",
            MenuOrder = 1,
        }, dataFilter);

        var role = await SaveAsync(new Module
        {
            ParentId = admin.Id,
            Name = "m_company_admin_role",
            ControllerName = "Role",
            Route = "company/roles",
            Icon = "group",
            MenuOrder = 1,
        }, dataFilter);

        var user = await SaveAsync(new Module
        {
            ParentId = admin.Id,
            Name = "m_company_admin_user",
            ControllerName = "User",
            Route = "company/users",
            Icon = "group",
            MenuOrder = 2,
        }, dataFilter);

        var permission = await SaveAsync(new Module
        {
            ParentId = admin.Id,
            Name = "m_company_admin_permission",
            ControllerName = "Permission",
            Route = "company/permissions",
            Icon = "group",
            MenuOrder = 3,
        }, dataFilter);
    }

    private async Task ApplyOnSavingBlAsync(Module entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnSavedBlAsync(Module entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnUpdatingBlAsync(Module existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnUpdatedBlAsync(Module existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnSoftDeletingBlAsync(Module existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnSoftDeletedBlAsync(Module existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(Module existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(Module existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(Module entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(ModuleFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(ModuleFilterModel filter, List<Expression<Func<Module, bool>>> predicates, DataFilter dataFilter)
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