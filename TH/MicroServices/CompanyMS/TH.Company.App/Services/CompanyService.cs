using System.Linq.Expressions;
using AutoMapper;
using MassTransit;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class CompanyService : BaseService, ICompanyService
{
    protected readonly IUow Repo;
    
	protected readonly IBranchUserService BranchUserService;
	protected readonly IBranchService BranchService;
	protected readonly IPermissionService PermissionService;
	protected readonly IRoleService RoleService;
	protected readonly IUserRoleService UserRoleService;
	protected readonly IUserService UserService;
        
    public CompanyService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IBranchUserService branchUserService, IBranchService branchService, IPermissionService permissionService, IRoleService roleService, IUserRoleService userRoleService, IUserService userService) : base(mapper,publishEndpoint)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        
		BranchUserService = branchUserService ?? throw new ArgumentNullException(nameof(branchUserService));
		BranchService = branchService ?? throw new ArgumentNullException(nameof(branchService));
		PermissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
		RoleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
		UserRoleService = userRoleService ?? throw new ArgumentNullException(nameof(userRoleService));
		UserService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<Company> SaveAsync(Company entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);
        await ApplyDuplicateOnSaveBl(entity, dataFilter);

        //Add your business logic here
        ApplyOnSavingBl(entity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.BranchUsers)
        {
            await BranchUserService.SaveAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.Branches)
        {
            await BranchService.SaveAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.Permissions)
        {
            await PermissionService.SaveAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.Roles)
        {
            await RoleService.SaveAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.UserRoles)
        {
            await UserRoleService.SaveAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.Users)
        {
            await UserService.SaveAsync(child, dataFilter, false);
        }
                
                
        await Repo.CompanyRepo.SaveAsync(entity);

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            ApplyOnSavedBl(entity, dataFilter);
        }

        return entity;
    }

    public async Task<Company> UpdateAsync(Company entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.CompanyRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(entity.SpaceId)) && (x.Id.Equals(entity.Id)));
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.Name = entity.Name;
		existingEntity.Code = entity.Code;
		existingEntity.Website = entity.Website;
		existingEntity.Slogan = entity.Slogan;
		existingEntity.Logo = entity.Logo;

        ApplyValidationBl(existingEntity);
        await ApplyDuplicateOnUpdateBl(existingEntity, dataFilter);

        //Add your business logic here
        ApplyOnUpdatingBl(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.BranchUsers)
        {
            await BranchUserService.UpdateAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.Branches)
        {
            await BranchService.UpdateAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.Permissions)
        {
            await PermissionService.UpdateAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.Roles)
        {
            await RoleService.UpdateAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.UserRoles)
        {
            await UserRoleService.UpdateAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.Users)
        {
            await UserService.UpdateAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("update_error"));

            //Add your business logic here
            ApplyOnUpdatedBl(existingEntity, dataFilter);
        }

        return existingEntity;
    }

    public async Task<bool> SoftDeleteAsync(Company entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.CompanyRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(entity.SpaceId)) && (x.Id.Equals(entity.Id)));
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        existingEntity.Active = false;

        //Add your business logic here
        ApplyOnDeletingBl(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in existingEntity.BranchUsers)
        {
            await BranchUserService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.Branches)
        {
            await BranchService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.Permissions)
        {
            await PermissionService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.Roles)
        {
            await RoleService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.UserRoles)
        {
            await UserRoleService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.Users)
        {
            await UserService.DeleteAsync(child, dataFilter, false);
        }
                

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            ApplyOnDeletedBl(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<bool> DeleteAsync(Company entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.CompanyRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(entity.SpaceId)) && (x.Id.Equals(entity.Id)));
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        //Add your business logic here
        ApplyOnDeletingBl(existingEntity, dataFilter);

        Repo.CompanyRepo.Delete(existingEntity);

        //Chain effect
        
        foreach (var child in existingEntity.BranchUsers)
        {
            await BranchUserService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.Branches)
        {
            await BranchService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.Permissions)
        {
            await PermissionService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.Roles)
        {
            await RoleService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.UserRoles)
        {
            await UserRoleService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.Users)
        {
            await UserService.DeleteAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            ApplyOnDeletedBl(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<Company> FindAsync(CompanyFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.CompanyRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(filter.SpaceId)) && (x.Id.Equals(filter.Id)));
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

    public async Task<IEnumerable<Company>> GetAsync(CompanyFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
            if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

            var predicates = new List<Expression<Func<Company, bool>>>();
            var includePredicates = new List<Expression<Func<Company, object>>>();

            //Add your business logic here
            ApplyOnGetBl(filter, dataFilter);

            

            #region Filters
            //Add your custom filter here
            ApplyCustomGetFilterBl(filter, predicates);
            
			if (!string.IsNullOrWhiteSpace(filter.Id)) predicates.Add(t => t.Id.Contains(filter.Id.Trim()));
			if (filter.CreatedDate.HasValue) predicates.Add(t => t.CreatedDate == filter.CreatedDate);
			if (filter.ModifiedDate.HasValue) predicates.Add(t => t.ModifiedDate == filter.ModifiedDate);
			if (filter.Active.HasValue) predicates.Add(t => t.Active == filter.Active);
			if (!string.IsNullOrWhiteSpace(filter.SpaceId)) predicates.Add(t => t.SpaceId.Contains(filter.SpaceId.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Name)) predicates.Add(t => t.Name.Contains(filter.Name.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Code)) predicates.Add(t => t.Code.Contains(filter.Code.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Website)) predicates.Add(t => t.Website.Contains(filter.Website.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Slogan)) predicates.Add(t => t.Slogan.Contains(filter.Slogan.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Logo)) predicates.Add(t => t.Logo.Contains(filter.Logo.Trim()));

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
				if (sortFilter.PropertyName.Equals("SpaceName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Space.Name";
            }

            #endregion

            var pagedList = await Repo.CompanyRepo.GetFilterableAsync(predicates, includePredicates, filter.SortFilters, filter.PageIndex, filter.PageSize, dataFilter);
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
            
			BranchUserService?.Dispose();
			BranchService?.Dispose();
			PermissionService?.Dispose();
			RoleService?.Dispose();
			UserRoleService?.Dispose();
			UserService?.Dispose();

            //Dispose additional services if any
            DisposeOthers();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region Business logic

    private void ApplyValidationBl(Company entity, bool skip = false)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException($"{Lang.Find("validation_error")}: Id") : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException($"{Lang.Find("validation_error")}: CreatedDate");
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException($"{Lang.Find("validation_error")}: ModifiedDate"); }
			entity.SpaceId = string.IsNullOrWhiteSpace(entity.SpaceId) ? throw new CustomException($"{Lang.Find("validation_error")}: SpaceId") : entity.SpaceId.Trim();
			entity.Name = string.IsNullOrWhiteSpace(entity.Name) ? throw new CustomException($"{Lang.Find("validation_error")}: Name") : entity.Name.Trim();
			entity.Code = string.IsNullOrWhiteSpace(entity.Code) ? Util.TryGenerateCode() : entity.Code.Trim();
			entity.Website = string.IsNullOrWhiteSpace(entity.Website) ? string.Empty : entity.Website.Trim();
			entity.Slogan = string.IsNullOrWhiteSpace(entity.Slogan) ? string.Empty : entity.Slogan.Trim();
			entity.Logo = string.IsNullOrWhiteSpace(entity.Logo) ? string.Empty : entity.Logo.Trim();
            
			if (entity.BranchUsers == null) entity.BranchUsers = new List<BranchUser>();
			if (entity.Branches == null) entity.Branches = new List<Branch>();
			if (entity.Permissions == null) entity.Permissions = new List<Permission>();
			if (entity.Roles == null) entity.Roles = new List<Role>();
			if (entity.UserRoles == null) entity.UserRoles = new List<UserRole>();
			if (entity.Users == null) entity.Users = new List<User>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnSaveBl(Company entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
		var existingEntityByName = await Repo.CompanyRepo.FindByNameAsync(entity.SpaceId, entity.Name, dataFilter);
		if (existingEntityByName is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Name");
		var existingEntityByCode = await Repo.CompanyRepo.FindByCodeAsync(entity.SpaceId, entity.Code, dataFilter);
		if (existingEntityByCode is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Code");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnUpdateBl(Company entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
		var existingEntityByName = await Repo.CompanyRepo.FindByNameExceptMeAsync(entity.Id, entity.SpaceId, entity.Name, dataFilter);
		if (existingEntityByName is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Name");
		var existingEntityByCode = await Repo.CompanyRepo.FindByCodeExceptMeAsync(entity.Id, entity.SpaceId, entity.Code, dataFilter);
		if (existingEntityByCode is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Code");
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}