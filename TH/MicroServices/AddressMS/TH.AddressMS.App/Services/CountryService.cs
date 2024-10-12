using System.Linq.Expressions;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using TH.AddressMS.Core;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.Io;

namespace TH.AddressMS.App;

public partial class CountryService : BaseService, ICountryService
{
    protected readonly IUow Repo;
    
	protected readonly IAddressService AddressService;
    
    //additional
    private readonly IExcelRepo _excelRepo;

    public CountryService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IAddressService addressService, IExcelRepo excelRepo, IConfiguration config) : base(mapper,publishEndpoint, config)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        
		AddressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        _excelRepo = excelRepo ?? throw new ArgumentNullException(nameof(excelRepo));
    }

    public async Task<Country> SaveAsync(Country entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);
        await ApplyDuplicateOnSaveBl(entity, dataFilter);

        //Add your business logic here
        await ApplyOnSavingBlAsync(entity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.Addresses)
        {
            await AddressService.SaveAsync(child, dataFilter, false);
        }
                
                
        await Repo.CountryRepo.SaveAsync(entity);

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("error_save"));

            //Add your business logic here
            await ApplyOnSavedBlAsync(entity, dataFilter);
        }

        return entity;
    }

    public async Task<Country> UpdateAsync(Country entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.CountryRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        
		existingEntity.Name = entity.Name;
		existingEntity.Code = entity.Code;
		existingEntity.IsoCode = entity.IsoCode;
		existingEntity.CurrencyName = entity.CurrencyName;
		existingEntity.CurrencyCode = entity.CurrencyCode;

        ApplyValidationBl(existingEntity);
        await ApplyDuplicateOnUpdateBl(existingEntity, dataFilter);

        //Add your business logic here
        await ApplyOnUpdatingBlAsync(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in entity.Addresses)
        {
            await AddressService.UpdateAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("update_error"));

            //Add your business logic here
            await ApplyOnUpdatedBlAsync(existingEntity, dataFilter);
        }

        return existingEntity;
    }

    public async Task<bool> SoftDeleteAsync(Country entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.CountryRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        existingEntity.Active = false;

        //Add your business logic here
        await ApplyOnSoftDeletingBlAsync(existingEntity, dataFilter);

        //Chain effect
        
        foreach (var child in existingEntity.Addresses)
        {
            await AddressService.DeleteAsync(child, dataFilter, false);
        }
                

        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            await ApplyOnSoftDeletedBlAsync(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<bool> DeleteAsync(Country entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.CountryRepo.FindByIdAsync(entity.Id, dataFilter);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        //Add your business logic here
        await ApplyOnDeletingBlAsync(existingEntity, dataFilter);

        Repo.CountryRepo.Delete(existingEntity);

        //Chain effect
        
        foreach (var child in existingEntity.Addresses)
        {
            await AddressService.DeleteAsync(child, dataFilter, false);
        }
                
                
        if (commit)
        {
            if (await Repo.SaveChangesAsync() <= 0) throw new CustomException(Lang.Find("delete_error"));

            //Add your business logic here
            await ApplyOnDeletedBlAsync(existingEntity, dataFilter);
        }

        return true;
    }

    public async Task<Country> FindByIdAsync(CountryFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var entity = await Repo.CountryRepo.FindByIdAsync(filter.Id, dataFilter);
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

    public async Task<IEnumerable<Country>> GetAsync(CountryFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
            if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

            var predicates = new List<Expression<Func<Country, bool>>>();
            var includePredicates = new List<Expression<Func<Country, object>>>();

            //Add your business logic here
            await ApplyOnGetBlAsync(filter, dataFilter);

            #region Filters
            //Add your custom filter here
            await ApplyCustomGetFilterBlAsync(filter, predicates, includePredicates, dataFilter);
            
			if (!string.IsNullOrWhiteSpace(filter.Id)) predicates.Add(t => t.Id.Contains(filter.Id.Trim()));
			if (filter.CreatedDate.HasValue) predicates.Add(t => t.CreatedDate == filter.CreatedDate);
			if (filter.ModifiedDate.HasValue) predicates.Add(t => t.ModifiedDate == filter.ModifiedDate);
			if (filter.Active.HasValue) predicates.Add(t => t.Active == filter.Active);
			if (!string.IsNullOrWhiteSpace(filter.Name)) predicates.Add(t => t.Name.Contains(filter.Name.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.Code)) predicates.Add(t => t.Code.Contains(filter.Code.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.IsoCode)) predicates.Add(t => t.IsoCode.Contains(filter.IsoCode.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.CurrencyName)) predicates.Add(t => t.CurrencyName.Contains(filter.CurrencyName.Trim()));
			if (!string.IsNullOrWhiteSpace(filter.CurrencyCode)) predicates.Add(t => t.CurrencyCode.Contains(filter.CurrencyCode.Trim()));

            #endregion

            #region Sort                

            foreach (var sortFilter in filter.SortFilters)
            {   
            }

            #endregion

            var pagedList = await Repo.CountryRepo.GetFilterableAsync(predicates, includePredicates, filter.SortFilters, filter.PageIndex, filter.PageSize, dataFilter);
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
            
			AddressService?.Dispose();

            //Dispose additional services if any
            DisposeOthers();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region Business logic

    private void ApplyValidationBl(Country entity)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
			entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException($"{Lang.Find("validation_error")}: Id") : entity.Id.Trim();
			if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException($"{Lang.Find("validation_error")}: CreatedDate");
			if (entity.ModifiedDate.HasValue) { if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException($"{Lang.Find("validation_error")}: ModifiedDate"); }
			entity.Name = string.IsNullOrWhiteSpace(entity.Name) ? throw new CustomException($"{Lang.Find("validation_error")}: Name") : entity.Name.Trim();
			entity.Code = string.IsNullOrWhiteSpace(entity.Code) ? Util.TryGenerateCode() : entity.Code.Trim();
			entity.IsoCode = string.IsNullOrWhiteSpace(entity.IsoCode) ? throw new CustomException($"{Lang.Find("validation_error")}: IsoCode") : entity.IsoCode.Trim();
			entity.CurrencyName = string.IsNullOrWhiteSpace(entity.CurrencyName) ? throw new CustomException($"{Lang.Find("validation_error")}: CurrencyName") : entity.CurrencyName.Trim();
			entity.CurrencyCode = string.IsNullOrWhiteSpace(entity.CurrencyCode) ? throw new CustomException($"{Lang.Find("validation_error")}: CurrencyCode") : entity.CurrencyCode.Trim();
            
			if (entity.Addresses == null) entity.Addresses = new List<Address>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnSaveBl(Country entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
			var existingEntityByName = await Repo.CountryRepo.FindByNameAsync(entity.Name, dataFilter);
			if (existingEntityByName is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Name");
			var existingEntityByIsoCode = await Repo.CountryRepo.FindByIsoCodeAsync(entity.IsoCode, dataFilter);
			if (existingEntityByIsoCode is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: IsoCode");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ApplyDuplicateOnUpdateBl(Country entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
		var existingEntityByName = await Repo.CountryRepo.FindByNameExceptMeAsync(entity.Id, entity.Name, dataFilter);
		if (existingEntityByName is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Name");
		 var existingEntityByCode = await Repo.CountryRepo.FindByCodeExceptMeAsync(entity.Id, entity.Code, dataFilter);
		if (existingEntityByCode is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Code");
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}