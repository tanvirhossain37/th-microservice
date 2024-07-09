using System.Linq.Expressions;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class BranchUserService : BaseService, IBranchUserService
{
    protected readonly IUow Repo;
    
        
    public BranchUserService(IUow repo) : base()
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        
    }

    public async Task<BranchUser> SaveAsync(BranchUser entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);

        //Add your business logic here
        ApplyOnSavingBl(entity, dataFilter);

        //Chain effect
        
                
        await Repo.BranchUserRepo.SaveAsync(entity);

        if (commit)
        {
            if (Repo.SaveChanges() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            ApplyOnSavedBl(entity, dataFilter);
        }

        return entity;
    }

    public async Task<BranchUser> UpdateAsync(BranchUser entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.BranchUserRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(entity.SpaceId)) && (x.CompanyId.Equals(entity.CompanyId)) && (x.Id.Equals(entity.Id)));
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.BranchId = entity.BranchId;
		existingEntity.UserId = entity.UserId;
		existingEntity.IsDefault = entity.IsDefault;

        ApplyValidationBl(existingEntity);

        //Add your business logic here
        ApplyOnUpdatingBl(existingEntity, dataFilter);

        //Chain effect
        
                
        if (commit)
        {
            if (Repo.SaveChanges() <= 0) throw new CustomException(Lang.Find("update_error"));

            //Add your business logic here
            ApplyOnUpdatedBl(existingEntity, dataFilter);
        }

        return existingEntity;
    }

    public async Task DeleteAsync(BranchUser entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.BranchUserRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(entity.SpaceId)) && (x.CompanyId.Equals(entity.CompanyId)) && (x.Id.Equals(entity.Id)));
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        existingEntity.Active = false;

        //Add your business logic here
        ApplyOnDeletingBl(existingEntity, dataFilter);

        //Chain effect
        
                
        if (commit)
        {
            if (Repo.SaveChanges() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            ApplyOnDeletedBl(existingEntity, dataFilter);
        }
    }

    public async Task<BranchUser> FindByIdAsync(BranchUserFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.BranchUserRepo.SingleOrDefaultQueryableAsync(x => (x.SpaceId.Equals(filter.SpaceId)) && (x.CompanyId.Equals(filter.CompanyId)) && (x.Id.Equals(filter.Id)));
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

    public async Task<IEnumerable<BranchUser>> GetAsync(BranchUserFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
            if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

            var predicates = new List<Expression<Func<BranchUser, bool>>>();
            var includePredicates = new List<Expression<Func<BranchUser, object>>>();

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
			if (!string.IsNullOrWhiteSpace(filter.BranchId)) predicates.Add(t => t.BranchId.Contains(filter.BranchId.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.UserId)) predicates.Add(t => t.UserId.Contains(filter.UserId.Trim()));
			if (filter.IsDefault.HasValue) predicates.Add(t => t.IsDefault == filter.IsDefault);

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
				if (sortFilter.PropertyName.Equals("SpaceName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Space.Name";
				if (sortFilter.PropertyName.Equals("CompanyName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Company.Name";
				if (sortFilter.PropertyName.Equals("BranchName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Branch.Name";
				if (sortFilter.PropertyName.Equals("UserName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "User.Name";
            }

            #endregion

            var pagedList = await Repo.BranchUserRepo.GetFilterableAsync(predicates, includePredicates, filter.SortFilters, filter.PageIndex, filter.PageSize, dataFilter);
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

    private void ApplyValidationBl(BranchUser entity, bool skip = false)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException(Lang.Find("validation_error")) : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException(Lang.Find("validation_error"));
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException(Lang.Find("validation_error")); }
			entity.SpaceId = string.IsNullOrWhiteSpace(entity.SpaceId) ? throw new CustomException(Lang.Find("validation_error")) : entity.SpaceId.Trim();
			entity.CompanyId = string.IsNullOrWhiteSpace(entity.CompanyId) ? throw new CustomException(Lang.Find("validation_error")) : entity.CompanyId.Trim();
			entity.BranchId = string.IsNullOrWhiteSpace(entity.BranchId) ? throw new CustomException(Lang.Find("validation_error")) : entity.BranchId.Trim();
			entity.UserId = string.IsNullOrWhiteSpace(entity.UserId) ? throw new CustomException(Lang.Find("validation_error")) : entity.UserId.Trim();
            
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}