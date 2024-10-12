using System.Linq.Expressions;
using AutoMapper;
using MassTransit;
using TH.CompanyMS.Core;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using Microsoft.Extensions.Configuration;

namespace TH.CompanyMS.App;

public partial class RoleService : BaseService, IRoleService
{
    protected readonly IUow Repo;
    
	protected readonly IPermissionService PermissionService;
	protected readonly IUserRoleService UserRoleService;

    public RoleService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IConfiguration config, IPermissionService permissionService, IUserRoleService userRoleService) : base(mapper, publishEndpoint, config)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));

        PermissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        UserRoleService = userRoleService ?? throw new ArgumentNullException(nameof(userRoleService));
    }

    public async Task<Role> SaveAsync(Role entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);
        await ApplyDuplicateOnSaveBl(entity, dataFilter);

        //Add your business logic here
        await ApplyOnSavingBlAsync(entity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.Permissions)
        {
            await PermissionService.SaveAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.UserRoles)
        {
            await UserRoleService.SaveAsync(child, dataFilter, false);
        }
                
                
        await Repo.RoleRepo.SaveAsync(entity);

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            await ApplyOnSavedBlAsync(entity, dataFilter);
        }

        return entity;
    }

    public async Task<Role> UpdateAsync(Role entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.RoleRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.Code = entity.Code;
		existingEntity.Name = entity.Name;

        ApplyValidationBl(existingEntity);
        await ApplyDuplicateOnUpdateBl(existingEntity, dataFilter);

        //Add your business logic here
        await ApplyOnUpdatingBlAsync(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.Permissions)
        {
            await PermissionService.UpdateAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.UserRoles)
        {
            await UserRoleService.UpdateAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("update_error"));

            //Add your business logic here
            await ApplyOnUpdatedBlAsync(existingEntity, dataFilter);
        }

        return existingEntity;
    }

    public async Task<bool> SoftDeleteAsync(Role entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.RoleRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        existingEntity.Active = false;

        //Add your business logic here
        await ApplyOnSoftDeletingBlAsync(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in existingEntity.Permissions)
        {
            await PermissionService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.UserRoles)
        {
            await UserRoleService.DeleteAsync(child, dataFilter, false);
        }
                

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            await ApplyOnSoftDeletedBlAsync(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<bool> DeleteAsync(Role entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.RoleRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        //Add your business logic here
        await ApplyOnDeletingBlAsync(existingEntity, dataFilter);

        Repo.RoleRepo.Delete(existingEntity);

        //Chain effect
        
        foreach (var child in existingEntity.Permissions)
        {
            await PermissionService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.UserRoles)
        {
            await UserRoleService.DeleteAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            await ApplyOnDeletedBlAsync(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<Role> FindByIdAsync(RoleFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.RoleRepo.FindByIdAsync(filter.Id, dataFilter);
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

    public async Task<IEnumerable<Role>> GetAsync(RoleFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
            if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

            var predicates = new List<Expression<Func<Role, bool>>>();
            var includePredicates = new List<Expression<Func<Role, object>>>();

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
			if (!string.IsNullOrWhiteSpace(filter.Code)) predicates.Add(t => t.Code.Contains(filter.Code.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Name)) predicates.Add(t => t.Name.Contains(filter.Name.Trim()));

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
				if (sortFilter.PropertyName.Equals("SpaceName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Space.Name";
				if (sortFilter.PropertyName.Equals("CompanyName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Company.Name";
            }

            #endregion

            var pagedList = await Repo.RoleRepo.GetFilterableAsync(predicates, includePredicates, filter.SortFilters, filter.PageIndex, filter.PageSize, dataFilter);
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
			UserRoleService?.Dispose();

            //Dispose additional services if any
            DisposeOthers();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region Business logic

    private void ApplyValidationBl(Role entity)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException($"{Lang.Find("validation_error")}: Id") : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException($"{Lang.Find("validation_error")}: CreatedDate");
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException($"{Lang.Find("validation_error")}: ModifiedDate"); }
			entity.SpaceId = string.IsNullOrWhiteSpace(entity.SpaceId) ? throw new CustomException($"{Lang.Find("validation_error")}: SpaceId") : entity.SpaceId.Trim();
			entity.CompanyId = string.IsNullOrWhiteSpace(entity.CompanyId) ? throw new CustomException($"{Lang.Find("validation_error")}: CompanyId") : entity.CompanyId.Trim();
			entity.Code = string.IsNullOrWhiteSpace(entity.Code) ? Util.TryGenerateCode() : entity.Code.Trim();
			entity.Name = string.IsNullOrWhiteSpace(entity.Name) ? throw new CustomException($"{Lang.Find("validation_error")}: Name") : entity.Name.Trim();
            
			if (entity.Permissions == null) entity.Permissions = new List<Permission>();
			if (entity.UserRoles == null) entity.UserRoles = new List<UserRole>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnSaveBl(Role entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
			var existingEntityByName = await Repo.RoleRepo.FindByNameAsync(entity.SpaceId, entity.CompanyId, entity.Name, dataFilter);
			if (existingEntityByName is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Name");
			var existingEntityByCode = await Repo.RoleRepo.FindByCodeAsync(entity.SpaceId, entity.CompanyId, entity.Code, dataFilter);
			if (existingEntityByCode is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Code");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnUpdateBl(Role entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
		var existingEntityByName = await Repo.RoleRepo.FindByNameExceptMeAsync(entity.Id, entity.SpaceId, entity.CompanyId, entity.Name, dataFilter);
		if (existingEntityByName is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Name");
		var existingEntityByCode = await Repo.RoleRepo.FindByCodeExceptMeAsync(entity.Id, entity.SpaceId, entity.CompanyId, entity.Code, dataFilter);
		if (existingEntityByCode is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Code");
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}