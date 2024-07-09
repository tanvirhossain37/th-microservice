using System.Linq.Expressions;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class ModuleService : BaseService, IModuleService
{
    protected readonly IUow Repo;
    
	//protected readonly IModuleService ModuleService;
	protected readonly IPermissionService PermissionService;
        
    public ModuleService(IUow repo, IPermissionService permissionService) : base()
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        
		PermissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
    }

    public async Task<Module> SaveAsync(Module entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);

        //Add your business logic here
        ApplyOnSavingBl(entity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.Permissions)
        {
            await PermissionService.SaveAsync(child, dataFilter, false);
        }
                
                
        await Repo.ModuleRepo.SaveAsync(entity);

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            ApplyOnSavedBl(entity, dataFilter);
        }

        return entity;
    }

    public async Task<Module> UpdateAsync(Module entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.ModuleRepo.SingleOrDefaultQueryableAsync(x => (x.Id.Equals(entity.Id)));
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.Name = entity.Name;
		existingEntity.Code = entity.Code;
		existingEntity.Route = entity.Route;
		existingEntity.Icon = entity.Icon;
		existingEntity.Level = entity.Level;
		existingEntity.ParentId = entity.ParentId;
		existingEntity.Order = entity.Order;

        ApplyValidationBl(existingEntity);

        //Add your business logic here
        ApplyOnUpdatingBl(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.Permissions)
        {
            await PermissionService.UpdateAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("update_error"));

            //Add your business logic here
            ApplyOnUpdatedBl(existingEntity, dataFilter);
        }

        return existingEntity;
    }

    public async Task<bool> SoftDeleteAsync(Module entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.ModuleRepo.SingleOrDefaultQueryableAsync(x => (x.Id.Equals(entity.Id)));
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        existingEntity.Active = false;

        //Add your business logic here
        ApplyOnDeletingBl(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in existingEntity.Permissions)
        {
            await PermissionService.DeleteAsync(child, dataFilter, false);
        }
                

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            ApplyOnDeletedBl(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<bool> DeleteAsync(Module entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.ModuleRepo.SingleOrDefaultQueryableAsync(x => (x.Id.Equals(entity.Id)));
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        //Add your business logic here
        ApplyOnDeletingBl(existingEntity, dataFilter);

        Repo.ModuleRepo.Delete(existingEntity);

        //Chain effect
        
        foreach (var child in existingEntity.Permissions)
        {
            await PermissionService.DeleteAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            ApplyOnDeletedBl(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<Module> FindAsync(ModuleFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.ModuleRepo.SingleOrDefaultQueryableAsync(x => (x.Id.Equals(filter.Id)));
            if (entity == null) throw new CustomException(Lang.Find("data_notfound"));

            //Add your business logic here
            ApplyOnFindByIdBl(entity, dataFilter);

            return entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Module>> GetAsync(ModuleFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
            if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

            var predicates = new List<Expression<Func<Module, bool>>>();
            var includePredicates = new List<Expression<Func<Module, object>>>();

            //Add your business logic here
            ApplyOnGetBl(filter, dataFilter);

            #region Filters
            //Add your custom filter here
            ApplyCustomGetFilterBl(filter, predicates);
            
			if (!string.IsNullOrWhiteSpace(filter.Id)) predicates.Add(t => t.Id.Contains(filter.Id.Trim()));
			if (filter.CreatedDate.HasValue) predicates.Add(t => t.CreatedDate == filter.CreatedDate);
			if (filter.ModifiedDate.HasValue) predicates.Add(t => t.ModifiedDate == filter.ModifiedDate);
			if (filter.Active.HasValue) predicates.Add(t => t.Active == filter.Active);
			if (!string.IsNullOrWhiteSpace(filter.Name)) predicates.Add(t => t.Name.Contains(filter.Name.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Code)) predicates.Add(t => t.Code.Contains(filter.Code.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Route)) predicates.Add(t => t.Route.Contains(filter.Route.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Icon)) predicates.Add(t => t.Icon.Contains(filter.Icon.Trim()));
			if (filter.Level > 0) predicates.Add(t => t.Level == filter.Level);
			if (!string.IsNullOrWhiteSpace(filter.ParentId)) predicates.Add(t => t.ParentId.Contains(filter.ParentId.Trim()));
			if (filter.Order > 0) predicates.Add(t => t.Order == filter.Order);

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
				if (sortFilter.PropertyName.Equals("ParentName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Parent.Name";
            }

            #endregion

            var pagedList = await Repo.ModuleRepo.GetFilterableAsync(predicates, includePredicates, filter.SortFilters, filter.PageIndex, filter.PageSize, dataFilter);
            if (!pagedList.Any()) throw new CustomException(Lang.Find("error_notfound"));

            return pagedList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override void Dispose()
    {
        try
        {
            Repo?.Dispose();
            
			PermissionService?.Dispose();

            //Dispose additional services if any
            DisposeOthers();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region Business logic

    private void ApplyValidationBl(Module entity, bool skip = false)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException(Lang.Find("validation_error")) : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException(Lang.Find("validation_error"));
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException(Lang.Find("validation_error")); }
			entity.Name = string.IsNullOrWhiteSpace(entity.Name) ? throw new CustomException(Lang.Find("validation_error")) : entity.Name.Trim();
			entity.Code = string.IsNullOrWhiteSpace(entity.Code) ? Util.TryGenerateCode() : entity.Code.Trim();
			entity.Route = string.IsNullOrWhiteSpace(entity.Route) ? string.Empty : entity.Route.Trim();
			entity.Icon = string.IsNullOrWhiteSpace(entity.Icon) ? string.Empty : entity.Icon.Trim();
			if (entity.Level <= 0) throw new CustomException(Lang.Find("validation_error"));
			entity.ParentId = string.IsNullOrWhiteSpace(entity.ParentId) ? string.Empty : entity.ParentId.Trim();
			if (entity.Order <= 0) throw new CustomException(Lang.Find("validation_error"));
            
			if (entity.InverseParent == null) entity.InverseParent = new List<Module>();
			if (entity.Permissions == null) entity.Permissions = new List<Permission>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}