using System.Linq.Expressions;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using TH.CompanyMS.Core;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;

namespace TH.CompanyMS.App;

public partial class UserService : BaseService, IUserService
{
    protected readonly IUow Repo;
    
	protected readonly IBranchUserService BranchUserService;
	protected readonly IUserCompanyService UserCompanyService;
	protected readonly IUserRoleService UserRoleService;

    //additional
    private AuthGrpcClientService _authGrpcClientService;

    public UserService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IConfiguration config, IBranchUserService branchUserService, IUserCompanyService userCompanyService, IUserRoleService userRoleService, AuthGrpcClientService authGrpcClientService) : base(mapper, publishEndpoint, config)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));

        BranchUserService = branchUserService ?? throw new ArgumentNullException(nameof(branchUserService));
        UserCompanyService = userCompanyService ?? throw new ArgumentNullException(nameof(userCompanyService));
        UserRoleService = userRoleService ?? throw new ArgumentNullException(nameof(userRoleService));
        _authGrpcClientService = authGrpcClientService ?? throw new ArgumentNullException(nameof(authGrpcClientService));
    }

    public async Task<User> SaveAsync(User entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);
        await ApplyDuplicateOnSaveBl(entity, dataFilter);

        //Add your business logic here
        await ApplyOnSavingBlAsync(entity, true, dataFilter);

        //Chain effect
        
        foreach (var child in entity.BranchUsers)
        {
            await BranchUserService.SaveAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.UserCompanies)
        {
            await UserCompanyService.SaveAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.UserRoles)
        {
            await UserRoleService.SaveAsync(child, dataFilter, false);
        }
                
                
        await Repo.UserRepo.SaveAsync(entity);

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            await ApplyOnSavedBlAsync(entity, dataFilter);
        }

        return entity;
    }

    public async Task<User> UpdateAsync(User entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.UserRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.UserTypeId = entity.UserTypeId;
		existingEntity.Name = entity.Name;
		existingEntity.UserName = entity.UserName;
		existingEntity.AccessTypeId = entity.AccessTypeId;

        ApplyValidationBl(existingEntity);
        await ApplyDuplicateOnUpdateBl(existingEntity, dataFilter);

        //Add your business logic here
        await ApplyOnUpdatingBlAsync(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.BranchUsers)
        {
            await BranchUserService.UpdateAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.UserCompanies)
        {
            await UserCompanyService.UpdateAsync(child, dataFilter, false);
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

    public async Task<bool> ArchiveAsync(User entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.UserRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        existingEntity.Active = false;

        //Add your business logic here
        await ApplyOnArchivingBlAsync(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in existingEntity.BranchUsers)
        {
            await BranchUserService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.UserCompanies)
        {
            await UserCompanyService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.UserRoles)
        {
            await UserRoleService.DeleteAsync(child, dataFilter, false);
        }
                

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            await ApplyOnArchivedBlAsync(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<bool> DeleteAsync(User entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.UserRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        //Add your business logic here
        await ApplyOnDeletingBlAsync(existingEntity, dataFilter);

        Repo.UserRepo.Delete(existingEntity);

        //Chain effect
        
        foreach (var child in existingEntity.BranchUsers)
        {
            await BranchUserService.DeleteAsync(child, dataFilter, false);
        }
                
        foreach (var child in existingEntity.UserCompanies)
        {
            await UserCompanyService.DeleteAsync(child, dataFilter, false);
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

    public async Task<User> FindByIdAsync(UserFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.UserRepo.FindByIdAsync(filter.Id, dataFilter);
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

    public async Task<IEnumerable<User>> GetAsync(UserFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
            if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

            var predicates = new List<Expression<Func<User, bool>>>();
            var includePredicates = new List<Expression<Func<User, object>>>();

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
			if (filter.UserTypeId > 0) predicates.Add(t => t.UserTypeId == filter.UserTypeId);
			if (!string.IsNullOrWhiteSpace(filter.Name)) predicates.Add(t => t.Name.Contains(filter.Name.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.UserName)) predicates.Add(t => t.UserName.Contains(filter.UserName.Trim()));
			if (filter.AccessTypeId > 0) predicates.Add(t => t.AccessTypeId == filter.AccessTypeId);

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
				if (sortFilter.PropertyName.Equals("SpaceName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Space.Name";
				if (sortFilter.PropertyName.Equals("CompanyName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Company.Name";
				if (sortFilter.PropertyName.Equals("UserTypeName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "UserType.Name";
				if (sortFilter.PropertyName.Equals("AccessTypeName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "AccessType.Name";
            }

            #endregion

            var pagedList = await Repo.UserRepo.GetFilterableAsync(predicates, includePredicates, filter.SortFilters, filter.PageIndex, filter.PageSize, dataFilter);
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
			UserCompanyService?.Dispose();
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

    private void ApplyValidationBl(User entity)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException($"{Lang.Find("validation_error")}: Id") : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException($"{Lang.Find("validation_error")}: CreatedDate");
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException($"{Lang.Find("validation_error")}: ModifiedDate"); }
			entity.SpaceId = string.IsNullOrWhiteSpace(entity.SpaceId) ? throw new CustomException($"{Lang.Find("validation_error")}: SpaceId") : entity.SpaceId.Trim();
			entity.CompanyId = string.IsNullOrWhiteSpace(entity.CompanyId) ? throw new CustomException($"{Lang.Find("validation_error")}: CompanyId") : entity.CompanyId.Trim();
			if (entity.UserTypeId <= 0) throw new CustomException($"{Lang.Find("validation_error")}: UserTypeId");
			entity.Name = string.IsNullOrWhiteSpace(entity.Name) ? throw new CustomException($"{Lang.Find("validation_error")}: Name") : entity.Name.Trim();
			entity.UserName = string.IsNullOrWhiteSpace(entity.UserName) ? throw new CustomException($"{Lang.Find("validation_error")}: UserName") : entity.UserName.Trim();
			if (entity.AccessTypeId <= 0) throw new CustomException($"{Lang.Find("validation_error")}: AccessTypeId");
            
			if (entity.BranchUsers == null) entity.BranchUsers = new List<BranchUser>();
			if (entity.UserCompanies == null) entity.UserCompanies = new List<UserCompany>();
			if (entity.UserRoles == null) entity.UserRoles = new List<UserRole>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnSaveBl(User entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
			var existingEntityByName = await Repo.UserRepo.FindByNameAsync(entity.SpaceId, entity.CompanyId, entity.Name, dataFilter);
			if (existingEntityByName is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Name");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnUpdateBl(User entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
		var existingEntityByName = await Repo.UserRepo.FindByNameExceptMeAsync(entity.Id, entity.SpaceId, entity.CompanyId, entity.Name, dataFilter);
		if (existingEntityByName is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Name");
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}