using System.Linq.Expressions;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class UserService : BaseService, IUserService
{
    protected readonly IUow Repo;
    
	protected readonly IBranchUserService BranchUserService;
	protected readonly IUserRoleService UserRoleService;
        
    public UserService(IUow repo, IBranchUserService branchUserService, IUserRoleService userRoleService) : base()
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        
		BranchUserService = branchUserService ?? throw new ArgumentNullException(nameof(branchUserService));
		UserRoleService = userRoleService ?? throw new ArgumentNullException(nameof(userRoleService));
    }

    public async Task<User> SaveAsync(User entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);

        //Add your business logic here
        ApplyOnSavingBl(entity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.BranchUsers)
        {
            await BranchUserService.SaveAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.UserRoles)
        {
            await UserRoleService.SaveAsync(child, dataFilter, false);
        }
                
                
        await Repo.UserRepo.SaveAsync(entity);

        if (commit)
        {
            if (Repo.SaveChanges() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            ApplyOnSavedBl(entity, dataFilter);
        }

        return entity;
    }

    public async Task<User> UpdateAsync(User entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.UserRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(entity.SpaceId)) && (x.CompanyId.Equals(entity.CompanyId)) && (x.Id.Equals(entity.Id)));
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.UserTypeId = entity.UserTypeId;
		existingEntity.Name = entity.Name;
		existingEntity.UserName = entity.UserName;

        ApplyValidationBl(existingEntity);

        //Add your business logic here
        ApplyOnUpdatingBl(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.BranchUsers)
        {
            await BranchUserService.UpdateAsync(child, dataFilter, false);
        }
                
        foreach (var child in entity.UserRoles)
        {
            await UserRoleService.UpdateAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (Repo.SaveChanges() <= 0) throw new CustomException(Lang.Find("update_error"));

            //Add your business logic here
            ApplyOnUpdatedBl(existingEntity, dataFilter);
        }

        return existingEntity;
    }

    public async Task DeleteAsync(User entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.UserRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(entity.SpaceId)) && (x.CompanyId.Equals(entity.CompanyId)) && (x.Id.Equals(entity.Id)));
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
                
        foreach (var child in existingEntity.UserRoles)
        {
            await UserRoleService.DeleteAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (Repo.SaveChanges() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            ApplyOnDeletedBl(existingEntity, dataFilter);
        }
    }

    public async Task<User> FindByIdAsync(UserFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.UserRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(filter.SpaceId)) && (x.CompanyId.Equals(filter.CompanyId)) && (x.Id.Equals(filter.Id)));
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
            ApplyOnGetBl(filter, dataFilter);

            #region Filters
            //Add your custom filter here
            ApplyCustomGetFilterBl(filter, predicates);
            
			if (!string.IsNullOrWhiteSpace(filter.Id)) predicates.Add(t => t.Id.Contains(filter.Id.Trim()));
			if (filter.CreatedDate.HasValue) predicates.Add(t => t.CreatedDate == filter.CreatedDate);
			if (filter.ModifiedDate.HasValue) predicates.Add(t => t.ModifiedDate == filter.ModifiedDate);
			if (filter.Active.HasValue) predicates.Add(t => t.Active == filter.Active);
			if (!string.IsNullOrWhiteSpace(filter.SpaceId)) predicates.Add(t => t.SpaceId.Contains(filter.SpaceId.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.CompanyId)) predicates.Add(t => t.CompanyId.Contains(filter.CompanyId.Trim()));
			if (filter.UserTypeId > 0) predicates.Add(t => t.UserTypeId == filter.UserTypeId);
			if (!string.IsNullOrWhiteSpace(filter.Name)) predicates.Add(t => t.Name.Contains(filter.Name.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.UserName)) predicates.Add(t => t.UserName.Contains(filter.UserName.Trim()));

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
				if (sortFilter.PropertyName.Equals("SpaceName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Space.Name";
				if (sortFilter.PropertyName.Equals("CompanyName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Company.Name";
				if (sortFilter.PropertyName.Equals("UserTypeName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "UserType.Name";
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

    private void ApplyValidationBl(User entity, bool skip = false)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException(Lang.Find("validation_error")) : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException(Lang.Find("validation_error"));
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException(Lang.Find("validation_error")); }
			entity.SpaceId = string.IsNullOrWhiteSpace(entity.SpaceId) ? throw new CustomException(Lang.Find("validation_error")) : entity.SpaceId.Trim();
			entity.CompanyId = string.IsNullOrWhiteSpace(entity.CompanyId) ? throw new CustomException(Lang.Find("validation_error")) : entity.CompanyId.Trim();
			if (entity.UserTypeId <= 0) throw new CustomException(Lang.Find("validation_error"));
			entity.Name = string.IsNullOrWhiteSpace(entity.Name) ? throw new CustomException(Lang.Find("validation_error")) : entity.Name.Trim();
			entity.UserName = string.IsNullOrWhiteSpace(entity.UserName) ? throw new CustomException(Lang.Find("validation_error")) : entity.UserName.Trim();
            
			if (entity.BranchUsers == null) entity.BranchUsers = new List<BranchUser>();
			if (entity.UserRoles == null) entity.UserRoles = new List<UserRole>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}