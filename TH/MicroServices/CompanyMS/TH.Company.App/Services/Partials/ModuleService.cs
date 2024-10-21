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

    private async Task ApplyOnArchivingBlAsync(Module existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivedBlAsync(Module existingEntity, DataFilter dataFilter)
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

    private async Task ApplyCustomGetFilterBlAsync(ModuleFilterModel filter, List<Expression<Func<Module, bool>>> predicates, List<Expression<Func<Module, object>>> includePredicates, DataFilter dataFilter)
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

        if (filter.ByTree.HasValue)
        {
            predicates.Add(x => x.ParentId == null);
            includePredicates.Add(x => x.InverseParent.OrderBy(m=>m.MenuOrder));
        }
    }

    public async Task InitAsync(DataFilter dataFilter)
    {
        #region Admin

        var admin = await SaveAsync(new Module
        {
            ParentId = null,
            Name = "m_admin",
            ControllerName = "",
            Route = "/company/admin",
            Icon = "admin_panel_settings",
            MenuOrder = 1,
            Level = 1
        }, dataFilter);

        #region Level 2

        var oleDashboard = await SaveAsync(new Module
        {
            ParentId = admin.Id,
            Name = "m_admin_dashboard",
            ControllerName = TS.Controllers.Company,
            Route = "/company/admin/dashboard",
            Icon = "dashboard",
            MenuOrder = 1,
            Level = 2
        }, dataFilter);

        var config = await SaveAsync(new Module
        {
            ParentId = admin.Id,
            Name = "m_admin_setting",
            ControllerName = TS.Controllers.CompanySetting,
            Route = "/company/admin/settings",
            Icon = "settings",
            MenuOrder = 2,
            Level = 2
        }, dataFilter);

        var role = await SaveAsync(new Module
        {
            ParentId = admin.Id,
            Name = "m_admin_role",
            ControllerName = TS.Controllers.Role,
            Route = "/company/admin/roles",
            Icon = "toggle_on",
            MenuOrder = 3,
            Level = 2
        }, dataFilter);

        var user = await SaveAsync(new Module
        {
            ParentId = admin.Id,
            Name = "m_admin_user",
            ControllerName = TS.Controllers.User,
            Route = "/company/admin/users",
            Icon = "group",
            MenuOrder = 4,
            Level = 2
        }, dataFilter);

        var permission = await SaveAsync(new Module
        {
            ParentId = admin.Id,
            Name = "m_admin_permission",
            ControllerName = TS.Controllers.Permission,
            Route = "/company/admin/permissions",
            Icon = "key",
            MenuOrder = 5,
            Level = 2
        }, dataFilter);

        #endregion

        #endregion

        #region Accounting

        var acounting = await SaveAsync(new Module
        {
            ParentId = null,
            Name = "m_accounting",
            ControllerName = "",
            Route = "/company/accounting",
            Icon = "currency_exchange",
            MenuOrder = 2,
            Level = 1
        }, dataFilter);

        #region Level 2

        await SaveAsync(new Module
        {
            ParentId = acounting.Id,
            Name = "m_accounting_dasboard",
            ControllerName = "",
            Route = "/company/accounting/dashboard",
            Icon = "dashboard",
            MenuOrder = 1,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = acounting.Id,
            Name = "m_accounting_setting",
            ControllerName = "",
            Route = "/company/accounting/settings",
            Icon = "settings",
            MenuOrder = 2,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = acounting.Id,
            Name = "m_accounting_coa",
            ControllerName = "",
            Route = "/company/accounting/coa",
            Icon = "account_tree",
            MenuOrder = 3,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = acounting.Id,
            Name = "m_accounting_posting",
            ControllerName = "",
            Route = "/company/accounting/postings",
            Icon = "post_add",
            MenuOrder = 4,
            Level = 2
        }, dataFilter);

        var report = await SaveAsync(new Module
        {
            ParentId = acounting.Id,
            Name = "m_accounting_report",
            ControllerName = "",
            Route = "/company/accounting/reports",
            Icon = "lab_profile",
            MenuOrder = 5,
            Level = 2
        }, dataFilter);

        #endregion

        #region Level 3

        await SaveAsync(new Module
        {
            ParentId = report.Id,
            Name = "m_accounting_report_ledger",
            ControllerName = "",
            Route = "/company/accounting/reports/ledger",
            Icon = "dehaze",
            MenuOrder = 1,
            Level = 3
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = report.Id,
            Name = "m_accounting_report_trialbalance",
            ControllerName = "",
            Route = "/company/accounting/reports/trialbalance",
            Icon = "heat_pump_balance",
            MenuOrder = 2,
            Level = 3
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = report.Id,
            Name = "m_accounting_report_incomestatement",
            ControllerName = "",
            Route = "/company/accounting/reports/incomestatement",
            Icon = "payments",
            MenuOrder = 3,
            Level = 3
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = report.Id,
            Name = "m_accounting_report_balancesheet",
            ControllerName = "",
            Route = "/company/accounting/reports/balancesheet",
            Icon = "balance",
            MenuOrder = 4,
            Level = 3
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = report.Id,
            Name = "m_accounting_report_cashflow",
            ControllerName = "",
            Route = "/company/accounting/reports/cashflow",
            Icon = "trending_up",
            MenuOrder = 5,
            Level = 3
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = report.Id,
            Name = "m_accounting_report_recieptpayment",
            ControllerName = "",
            Route = "/company/accounting/reports/recieptpayment",
            Icon = "receipt_long",
            MenuOrder = 6,
            Level = 3
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = report.Id,
            Name = "m_accounting_report_costprofitanalysis",
            ControllerName = "",
            Route = "/company/accounting/reports/costprofitanalysis",
            Icon = "compare",
            MenuOrder = 7,
            Level = 3
        }, dataFilter);

        #endregion

        #endregion

        #region HR

        var hr = await SaveAsync(new Module
        {
            ParentId = null,
            Name = "m_hr",
            ControllerName = "",
            Route = "/company/hr",
            Icon = "diversity_3",
            MenuOrder = 3,
            Level = 1
        }, dataFilter);

        #region Level 2

        await SaveAsync(new Module
        {
            ParentId = hr.Id,
            Name = "m_hr_dashboard",
            ControllerName = "",
            Route = "/company/hr/dashboard",
            Icon = "dashboard",
            MenuOrder = 1,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = hr.Id,
            Name = "m_hr_setting",
            ControllerName = TS.Controllers.Company,
            Route = "/company/hr/settings",
            Icon = "settings",
            MenuOrder = 2,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = hr.Id,
            Name = "m_hr_employee",
            ControllerName = TS.Controllers.Company,
            Route = "/company/hr/employees",
            Icon = "supervisor_account",
            MenuOrder = 3,
            Level = 2
        }, dataFilter);

        #endregion

        #endregion

        #region Inventory

        var inventory = await SaveAsync(new Module
        {
            ParentId = null,
            Name = "m_inventory",
            ControllerName = "",
            Route = "/company/inventories",
            Icon = "inventory",
            MenuOrder = 4,
            Level = 1
        }, dataFilter);

        #region Level 2

        await SaveAsync(new Module
        {
            ParentId = inventory.Id,
            Name = "m_inventory_dashboard",
            ControllerName = "",
            Route = "/company/inventories/dashboard",
            Icon = "dashboard",
            MenuOrder = 1,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = inventory.Id,
            Name = "m_inventory_setting",
            ControllerName = "",
            Route = "/company/inventories/settings",
            Icon = "settings",
            MenuOrder = 2,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = inventory.Id,
            Name = "m_inventory_product",
            ControllerName = "",
            Route = "/company/inventories/products",
            Icon = "align_items_stretch",
            MenuOrder = 3,
            Level = 2
        }, dataFilter);

        #endregion

        #endregion

        #region Supply

        var supply = await SaveAsync(new Module
        {
            ParentId = null,
            Name = "m_supply",
            ControllerName = "",
            Route = "/company/supplies",
            Icon = "inventory_2",
            MenuOrder = 5,
            Level = 1
        }, dataFilter);

        #region Level 2

        await SaveAsync(new Module
        {
            ParentId = supply.Id,
            Name = "m_supply_dashboard",
            ControllerName = "",
            Route = "/company/supplies/dashboard",
            Icon = "dashboard",
            MenuOrder = 1,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = supply.Id,
            Name = "m_supply_setting",
            ControllerName = "",
            Route = "/company/supplies/settings",
            Icon = "settings",
            MenuOrder = 2,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = inventory.Id,
            Name = "m_supply_employee",
            ControllerName = "",
            Route = "/company/supplies/products",
            Icon = "align_items_stretch",
            MenuOrder = 3,
            Level = 2
        }, dataFilter);

        #endregion

        #endregion

        #region FA

        var fa = await SaveAsync(new Module
        {
            ParentId = null,
            Name = "m_fa",
            ControllerName = "",
            Route = "/company/fa",
            Icon = "web_asset",
            MenuOrder = 6,
            Level = 1
        }, dataFilter);

        #region Level 2

        await SaveAsync(new Module
        {
            ParentId = fa.Id,
            Name = "m_fa_dashboard",
            ControllerName = "",
            Route = "/company/fa/dashboard",
            Icon = "dashboard",
            MenuOrder = 1,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = fa.Id,
            Name = "m_fa_setting",
            ControllerName = "",
            Route = "/company/fa/settings",
            Icon = "settings",
            MenuOrder = 2,
            Level = 2
        }, dataFilter);

        await SaveAsync(new Module
        {
            ParentId = inventory.Id,
            Name = "m_fa_product",
            ControllerName = "",
            Route = "/company/fa/products",
            Icon = "align_items_stretch",
            MenuOrder = 3,
            Level = 2
        }, dataFilter);

        #endregion

        #endregion
    }

    private void DisposeOthers()
    {
        //todo
    }
}