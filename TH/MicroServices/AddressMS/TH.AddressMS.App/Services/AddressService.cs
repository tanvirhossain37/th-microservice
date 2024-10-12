using System.Linq.Expressions;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using TH.AddressMS.Core;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;

namespace TH.AddressMS.App;

public partial class AddressService : BaseService, IAddressService
{
    protected readonly IUow Repo;
    
        
    public AddressService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IConfiguration config) : base(mapper,publishEndpoint, config)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        
    }

    public async Task<Address> SaveAsync(Address entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);
        await ApplyDuplicateOnSaveBl(entity, dataFilter);

        //Add your business logic here
        await ApplyOnSavingBlAsync(entity, dataFilter);

        //Chain effect
        
                
        await Repo.AddressRepo.SaveAsync(entity);

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            await ApplyOnSavedBlAsync(entity, dataFilter);
        }

        return entity;
    }

    public async Task<Address> UpdateAsync(Address entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.AddressRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.Street = entity.Street;
		existingEntity.City = entity.City;
		existingEntity.State = entity.State;
		existingEntity.PostalCode = entity.PostalCode;
		existingEntity.CountryId = entity.CountryId;
		existingEntity.ClientId = entity.ClientId;

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

    public async Task<bool> SoftDeleteAsync(Address entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.AddressRepo.FindByIdAsync(entity.Id, dataFilter);
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

    public async Task<bool> DeleteAsync(Address entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.AddressRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        //Add your business logic here
        await ApplyOnDeletingBlAsync(existingEntity, dataFilter);

        Repo.AddressRepo.Delete(existingEntity);

        //Chain effect
        
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            await ApplyOnDeletedBlAsync(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<Address> FindByIdAsync(AddressFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.AddressRepo.FindByIdAsync(filter.Id, dataFilter);
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

    public async Task<IEnumerable<Address>> GetAsync(AddressFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
            if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

            var predicates = new List<Expression<Func<Address, bool>>>();
            var includePredicates = new List<Expression<Func<Address, object>>>();

            //Add your business logic here
            await ApplyOnGetBlAsync(filter, dataFilter);

            #region Filters
            //Add your custom filter here
            await ApplyCustomGetFilterBlAsync(filter, predicates, includePredicates, dataFilter);
            
			if (!string.IsNullOrWhiteSpace(filter.Id)) predicates.Add(t => t.Id.Contains(filter.Id.Trim()));
			if (filter.CreatedDate.HasValue) predicates.Add(t => t.CreatedDate == filter.CreatedDate);
			if (filter.ModifiedDate.HasValue) predicates.Add(t => t.ModifiedDate == filter.ModifiedDate);
			if (filter.Active.HasValue) predicates.Add(t => t.Active == filter.Active);
			if (!string.IsNullOrWhiteSpace(filter.Street)) predicates.Add(t => t.Street.Contains(filter.Street.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.City)) predicates.Add(t => t.City.Contains(filter.City.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.State)) predicates.Add(t => t.State.Contains(filter.State.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.PostalCode)) predicates.Add(t => t.PostalCode.Contains(filter.PostalCode.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.CountryId)) predicates.Add(t => t.CountryId.Contains(filter.CountryId.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.ClientId)) predicates.Add(t => t.ClientId.Contains(filter.ClientId.Trim()));

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
				if (sortFilter.PropertyName.Equals("CountryName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Country.Name";
				if (sortFilter.PropertyName.Equals("ClientName", StringComparison.InvariantCultureIgnoreCase)) sortFilter.PropertyName = "Client.Name";
            }

            #endregion

            var pagedList = await Repo.AddressRepo.GetFilterableAsync(predicates, includePredicates, filter.SortFilters, filter.PageIndex, filter.PageSize, dataFilter);
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

    private void ApplyValidationBl(Address entity)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException($"{Lang.Find("validation_error")}: Id") : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException($"{Lang.Find("validation_error")}: CreatedDate");
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException($"{Lang.Find("validation_error")}: ModifiedDate"); }
			entity.Street = string.IsNullOrWhiteSpace(entity.Street) ? throw new CustomException($"{Lang.Find("validation_error")}: Street") : entity.Street.Trim();
			entity.City = string.IsNullOrWhiteSpace(entity.City) ? throw new CustomException($"{Lang.Find("validation_error")}: City") : entity.City.Trim();
			entity.State = string.IsNullOrWhiteSpace(entity.State) ? string.Empty : entity.State.Trim();
			entity.PostalCode = string.IsNullOrWhiteSpace(entity.PostalCode) ? throw new CustomException($"{Lang.Find("validation_error")}: PostalCode") : entity.PostalCode.Trim();
			entity.CountryId = string.IsNullOrWhiteSpace(entity.CountryId) ? throw new CustomException($"{Lang.Find("validation_error")}: CountryId") : entity.CountryId.Trim();
			entity.ClientId = string.IsNullOrWhiteSpace(entity.ClientId) ? throw new CustomException($"{Lang.Find("validation_error")}: ClientId") : entity.ClientId.Trim();
            
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnSaveBl(Address entity, DataFilter dataFilter)
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

    private async Task ApplyDuplicateOnUpdateBl(Address entity, DataFilter dataFilter)
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