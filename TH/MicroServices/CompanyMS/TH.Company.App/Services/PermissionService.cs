using System.Linq.Expressions;
using AutoMapper;
using MassTransit;
using TH.CompanyMS.Core;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using Microsoft.Extensions.Configuration;

namespace TH.CompanyMS.App;

public partial class PermissionService : BaseService, IPermissionService
{
    protected readonly IUow Repo;

    //protected readonly IPermissionService PermissionService;

    public PermissionService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IConfiguration config) : base(mapper, publishEndpoint, config)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));

    }

    public async Task<Permission> SaveAsync(Permission entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);
        await ApplyDuplicateOnSaveBl(entity, dataFilter);

        //Add your business logic here
        await ApplyOnSavingBlAsync(entity, dataFilter);

        //Chain effect
        
                
        await Repo.PermissionRepo.SaveAsync(entity);

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            await ApplyOnSavedBlAsync(entity, dataFilter);
        }

        return entity;
    }

    public async Task<Permission> UpdateAsync(Permission entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.PermissionRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.RoleId = entity.RoleId;
		existingEntity.ModuleId = entity.ModuleId;
		existingEntity.Read = entity.Read;
		existingEntity.Write = entity.Write;
		existingEntity.Update = entity.Update;
		existingEntity.Delete = entity.Delete;
		existingEntity.ParentId = entity.ParentId;
		existingEntity.MenuOrder = entity.MenuOrder;

        ApplyValidationBl(existingEntity);
        await ApplyDuplicateOnUpdateBl(existingEntity, dataFilter);

        //Add your business logic here
        await ApplyOnUpdatingBlAsync(existingEntity, dataFilter);

        //Chain effect
        
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("update_error"));

            //Add your business logic here
            await ApplyOnUpdatedBlAsync(existingEntity, dataFilter);
        }

        return existingEntity;
    }

    public async Task<bool> SoftDeleteAsync(Permission entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.PermissionRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        existingEntity.Active = false;

        //Add your business logic here
        await ApplyOnSoftDeletingBlAsync(existingEntity, dataFilter);

        //Chain effect
        

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            await ApplyOnSoftDeletedBlAsync(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<bool> DeleteAsync(Permission entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.PermissionRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        //Add your business logic here
        await ApplyOnDeletingBlAsync(existingEntity, dataFilter);

        Repo.PermissionRepo.Delete(existingEntity);

        //Chain effect
        
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            await ApplyOnDeletedBlAsync(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<Permission> FindByIdAsync(PermissionFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.PermissionRepo.FindByIdAsync(filter.Id, dataFilter);
            if (entity == null) throw new CustomException(Lang.Find("data_notfound"));

            //Add your business logic here
            await ApplyOnFindBlAsync(entity, dataFilter);

            return entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Permission>> GetAsync(PermissionFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
            if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

            var predicates = new List<Expression<Func<Permission, bool>>>();
            var includePredicates = new List<Expression<Func<Permission, object>>>();

            //Add your business logic here
            await ApplyOnGetBlAsync(filter, dataFilter);

            #region Filters
            //Add your custom filter here
            await ApplyCustomGetFilterBlAsync(filter, predicates, includePredicates, dataFilter);
            
			if (!string.IsNullOrWhiteSpace(filter.Id)) predicates.Add(t => t.Id.Contains(filter.Id.Trim()));
			if (filter.CreatedDate.HasValue) predicates.Add(t => t.CreatedDate == filter.CreatedDate);
			if (filter.ModifiedDate.HasValue) predicates.Add(t => t.ModifiedDate == filter.ModifiedDate);
			if (filter.Active.HasValue) predicates.Add(t => t.Active == filter.Active);
			if (!string.IsNullOrWhiteSpace(filter.SpaceId)) predicates.Add(t => t.SpaceId.Contains(filter.SpaceId.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.CompanyId)) predicates.Add(t => t.CompanyId.Contains(filter.CompanyId.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.RoleId)) predicates.Add(t => t.RoleId.Contains(filter.RoleId.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.ModuleId)) predicates.Add(t => t.ModuleId.Contains(filter.ModuleId.Trim()));
			if (filter.Read.HasValue) predicates.Add(t => t.Read == filter.Read);
			if (filter.Write.HasValue) predicates.Add(t => t.Write == filter.Write);
			if (filter.Update.HasValue) predicates.Add(t => t.Update == filter.Update);
			if (filter.Delete.HasValue) predicates.Add(t => t.Delete == filter.Delete);
			if (!string.IsNullOrWhiteSpace(filter.ParentId)) predicates.Add(t => t.ParentId.Contains(filter.ParentId.Trim()));
			if (filter.MenuOrder > 0) predicates.Add(t => t.MenuOrder == filter.MenuOrder);

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
				if (sortFilter.PropertyName.Equals("SpaceName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Space.Name";
				if (sortFilter.PropertyName.Equals("CompanyName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Company.Name";
				if (sortFilter.PropertyName.Equals("RoleName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Role.Name";
				if (sortFilter.PropertyName.Equals("ModuleName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Module.Name";
				if (sortFilter.PropertyName.Equals("ParentName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Parent.Name";
            }

            #endregion

            var pagedList = await Repo.PermissionRepo.GetFilterableAsync(predicates, includePredicates, filter.SortFilters, filter.PageIndex, filter.PageSize, dataFilter);
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
            

            //Dispose additional services if any
            DisposeOthers();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region Business logic

    private void ApplyValidationBl(Permission entity)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException($"{Lang.Find("validation_error")}: Id") : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException($"{Lang.Find("validation_error")}: CreatedDate");
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException($"{Lang.Find("validation_error")}: ModifiedDate"); }
			entity.SpaceId = string.IsNullOrWhiteSpace(entity.SpaceId) ? throw new CustomException($"{Lang.Find("validation_error")}: SpaceId") : entity.SpaceId.Trim();
			entity.CompanyId = string.IsNullOrWhiteSpace(entity.CompanyId) ? throw new CustomException($"{Lang.Find("validation_error")}: CompanyId") : entity.CompanyId.Trim();
			entity.RoleId = string.IsNullOrWhiteSpace(entity.RoleId) ? throw new CustomException($"{Lang.Find("validation_error")}: RoleId") : entity.RoleId.Trim();
			entity.ModuleId = string.IsNullOrWhiteSpace(entity.ModuleId) ? throw new CustomException($"{Lang.Find("validation_error")}: ModuleId") : entity.ModuleId.Trim();
			entity.ParentId = string.IsNullOrWhiteSpace(entity.ParentId) ? null : entity.ParentId.Trim();
			if (entity.MenuOrder <= 0) throw new CustomException($"{Lang.Find("validation_error")}: MenuOrder");
            
			if (entity.InverseParent == null) entity.InverseParent = new List<Permission>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnSaveBl(Permission entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnUpdateBl(Permission entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}