using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using TH.AddressMS.Core;
using TH.Common.Model;
using TH.Common.Util;
using TH.Io;

namespace TH.AddressMS.App;

public partial class AddressService
{
    //Add additional services if any
    private IExcelRepo _excelRepo;

    public AddressService(IUow repo, IPublishEndpoint publishEndpoint, IMapper mapper, IConfiguration config, IExcelRepo excelRepo) : this(repo, publishEndpoint, mapper, config)
    {
        _excelRepo = excelRepo ?? throw new ArgumentNullException(nameof(excelRepo));
    }

    private async Task ApplyOnSavingBlAsync(Address entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
        var defaultEntity = await Repo.AddressRepo.SingleOrDefaultQueryableAsync(x => x.ClientId.Equals(entity.ClientId) && x.IsDefault == true, dataFilter);
        if (defaultEntity == null)//no data
        {
            entity.IsDefault = true;
        }
        else
        {
            if (entity.IsDefault)
            {
                defaultEntity.IsDefault = false;
            }
        }
    }

    private async Task ApplyOnSavedBlAsync(Address entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnUpdatingBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
        var defaultEntity = await Repo.AddressRepo.SingleOrDefaultQueryableAsync(x => !x.ClientId.Equals(existingEntity.Id) && x.IsDefault == true, dataFilter);
        if (defaultEntity == null)//no data
        {
            existingEntity.IsDefault = true;
        }
        else
        {
            if (existingEntity.IsDefault)
            {
                defaultEntity.IsDefault = false;
            }
        }
    }

    private async Task ApplyOnUpdatedBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivingBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivedBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(Address existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(Address entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(AddressFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(AddressFilterModel filter, List<Expression<Func<Address, bool>>> predicates, List<Expression<Func<Address, object>>> includePredicates, DataFilter dataFilter)
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
}