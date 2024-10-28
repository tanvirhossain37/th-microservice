using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using TH.AddressMS.Core;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.Io;

namespace TH.AddressMS.App;

public partial class CountryService
{
    //Add additional services if any
    private IExcelRepo _excelRepo;

    public CountryService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IConfiguration config, IAddressService addressService, IExcelRepo excelRepo) : this(repo, publishEndpoint, mapper, config, addressService)
    {
        _excelRepo = excelRepo ?? throw new ArgumentNullException(nameof(excelRepo));
    }

    private async Task ApplyOnSavingBlAsync(Country entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnSavedBlAsync(Country entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnUpdatingBlAsync(Country existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnUpdatedBlAsync(Country existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivingBlAsync(Country existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivedBlAsync(Country existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(Country existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(Country existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(Country entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(CountryFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(CountryFilterModel filter, List<Expression<Func<Country, bool>>> predicates,
        List<Expression<Func<Country, object>>> includePredicates, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (predicates == null) throw new ArgumentNullException(nameof(predicates));

        //todo
        //additional
        if (filter.StartDate.HasValue && filter.EndDate.HasValue)
        {
            filter.StartDate = Util.TryFloorTime((DateTime)filter.StartDate);
            filter.EndDate = Util.TryCeilTime((DateTime)filter.EndDate);

            predicates.Add(t => (t.CreatedDate >= filter.StartDate) && (t.CreatedDate <= filter.EndDate));
        }
    }

    private void DisposeOthers()
    {
        //todo
    }

    public async Task<ExcelResult> TryInitAsync(string path)
    {
        var result = new ExcelResult();

        var rows = _excelRepo.Fetch<CountryExcel>(path);

       result.TotalRowCount = rows.ToList().Count();

        var rowNumber = 1;

        foreach (var row in rows)
        {
            ApplyExcelValidationBl(row);

            var newCountry = new Country
            {
                Name = $"country.c_{row.Country.ToLower()}",
                Code = row.CountryCode,
                IsoCode = row.IsoCode.Split("/")[0].Trim(),
                CurrencyName = $"currency.cr_{Util.ToUnderscoreCase(row.CurrencyName)}",
                CurrencyCode = row.CurrencyCode
            };

            try
            {
                await SaveAsync(newCountry, new DataFilter { IncludeInactive = false });
                result.SuccessCount++;
                
                rowNumber++;
            }
            catch (Exception e)
            {
                result.ErrorCount++;
                result.ErrorDetails.Add(new ErrorDetail
                {
                    RowNumber = rowNumber,
                    Message = e.Message,
                    Row = row
                });
            }
        }

        return result;
    }

    private void ApplyExcelValidationBl(CountryExcel row)
    {
        if (row == null) throw new ArgumentNullException(nameof(row));


        row.Country=string.IsNullOrWhiteSpace(row.Country)? throw new CustomException($"{Lang.Find("validation_error")}: Country") : row.Country.Trim();
        row.CountryCode= string.IsNullOrWhiteSpace(row.CountryCode) ? throw new CustomException($"{Lang.Find("validation_error")}: CountryCode") : row.CountryCode.Trim();
        row.IsoCode= string.IsNullOrWhiteSpace(row.IsoCode) ? throw new CustomException($"{Lang.Find("validation_error")}: IsoCode") : row.IsoCode.Trim();
        row.CurrencyName= string.IsNullOrWhiteSpace(row.CurrencyName) ? throw new CustomException($"{Lang.Find("validation_error")}: CurrencyName") : row.CurrencyName.Trim();
        row.CurrencyCode= string.IsNullOrWhiteSpace(row.CurrencyCode) ? throw new CustomException($"{Lang.Find("validation_error")}: CurrencyCode") : row.CurrencyCode.Trim();
    }
}