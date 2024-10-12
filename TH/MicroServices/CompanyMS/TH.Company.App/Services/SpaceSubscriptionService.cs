using System.Linq.Expressions;
using AutoMapper;
using MassTransit;
using TH.CompanyMS.Core;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using Microsoft.Extensions.Configuration;

namespace TH.CompanyMS.App;

public partial class SpaceSubscriptionService : BaseService, ISpaceSubscriptionService
{
    protected readonly IUow Repo;


    public SpaceSubscriptionService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IConfiguration config) : base(mapper, publishEndpoint, config)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));

    }

    public async Task<SpaceSubscription> SaveAsync(SpaceSubscription entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);
        await ApplyDuplicateOnSaveBl(entity, dataFilter);

        //Add your business logic here
        await ApplyOnSavingBlAsync(entity, dataFilter);

        //Chain effect
        
                
        await Repo.SpaceSubscriptionRepo.SaveAsync(entity);

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            await ApplyOnSavedBlAsync(entity, dataFilter);
        }

        return entity;
    }

    public async Task<SpaceSubscription> UpdateAsync(SpaceSubscription entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.SpaceSubscriptionRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.PlanId = entity.PlanId;
		existingEntity.IsCurrent = entity.IsCurrent;
		existingEntity.CardHolderName = entity.CardHolderName;
		existingEntity.CardNumber = entity.CardNumber;
		existingEntity.SecurityCode = entity.SecurityCode;
		existingEntity.CardExpiryDate = entity.CardExpiryDate;
		existingEntity.CountryId = entity.CountryId;
		existingEntity.ZipCode = entity.ZipCode;

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

    public async Task<bool> SoftDeleteAsync(SpaceSubscription entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.SpaceSubscriptionRepo.FindByIdAsync(entity.Id, dataFilter);
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

    public async Task<bool> DeleteAsync(SpaceSubscription entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.SpaceSubscriptionRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        //Add your business logic here
        await ApplyOnDeletingBlAsync(existingEntity, dataFilter);

        Repo.SpaceSubscriptionRepo.Delete(existingEntity);

        //Chain effect
        
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            await ApplyOnDeletedBlAsync(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<SpaceSubscription> FindByIdAsync(SpaceSubscriptionFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.SpaceSubscriptionRepo.FindByIdAsync(filter.Id, dataFilter);
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

    public async Task<IEnumerable<SpaceSubscription>> GetAsync(SpaceSubscriptionFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
            if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

            var predicates = new List<Expression<Func<SpaceSubscription, bool>>>();
            var includePredicates = new List<Expression<Func<SpaceSubscription, object>>>();

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
			if (filter.PlanId > 0) predicates.Add(t => t.PlanId == filter.PlanId);
			if (filter.IsCurrent.HasValue) predicates.Add(t => t.IsCurrent == filter.IsCurrent);
			if (!string.IsNullOrWhiteSpace(filter.CardHolderName)) predicates.Add(t => t.CardHolderName.Contains(filter.CardHolderName.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.CardNumber)) predicates.Add(t => t.CardNumber.Contains(filter.CardNumber.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.SecurityCode)) predicates.Add(t => t.SecurityCode.Contains(filter.SecurityCode.Trim()));
			if (filter.CardExpiryDate.HasValue) predicates.Add(t => t.CardExpiryDate == filter.CardExpiryDate);
			if (!string.IsNullOrWhiteSpace(filter.CountryId)) predicates.Add(t => t.CountryId.Contains(filter.CountryId.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.ZipCode)) predicates.Add(t => t.ZipCode.Contains(filter.ZipCode.Trim()));

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
				if (sortFilter.PropertyName.Equals("SpaceName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Space.Name";
				if (sortFilter.PropertyName.Equals("PlanName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Plan.Name";
				if (sortFilter.PropertyName.Equals("CountryName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Country.Name";
            }

            #endregion

            var pagedList = await Repo.SpaceSubscriptionRepo.GetFilterableAsync(predicates, includePredicates, filter.SortFilters, filter.PageIndex, filter.PageSize, dataFilter);
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

    private void ApplyValidationBl(SpaceSubscription entity)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException($"{Lang.Find("validation_error")}: Id") : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException($"{Lang.Find("validation_error")}: CreatedDate");
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException($"{Lang.Find("validation_error")}: ModifiedDate"); }
			entity.SpaceId = string.IsNullOrWhiteSpace(entity.SpaceId) ? throw new CustomException($"{Lang.Find("validation_error")}: SpaceId") : entity.SpaceId.Trim();
			if (entity.PlanId <= 0) throw new CustomException($"{Lang.Find("validation_error")}: PlanId");
			entity.CardHolderName = string.IsNullOrWhiteSpace(entity.CardHolderName) ? string.Empty : entity.CardHolderName.Trim();
			entity.CardNumber = string.IsNullOrWhiteSpace(entity.CardNumber) ? string.Empty : entity.CardNumber.Trim();
			entity.SecurityCode = string.IsNullOrWhiteSpace(entity.SecurityCode) ? string.Empty : entity.SecurityCode.Trim();
			if (entity.CardExpiryDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.CardExpiryDate)) throw new CustomException($"{Lang.Find("validation_error")}: CardExpiryDate"); }
			entity.CountryId = string.IsNullOrWhiteSpace(entity.CountryId) ? string.Empty : entity.CountryId.Trim();
			entity.ZipCode = string.IsNullOrWhiteSpace(entity.ZipCode) ? string.Empty : entity.ZipCode.Trim();
            
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnSaveBl(SpaceSubscription entity, DataFilter dataFilter)
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

    private async Task ApplyDuplicateOnUpdateBl(SpaceSubscription entity, DataFilter dataFilter)
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