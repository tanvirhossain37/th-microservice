using System.Linq.Expressions;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class BranchService
{
    //Add additional services if any

    //public BranchService(IUow repo, IBranchUserService branchUserService) : this()
    //{
    //}

    private async Task ApplyOnSavingBlAsync(Branch entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
        var defaultEntity = await Repo.BranchRepo.SingleOrDefaultQueryableAsync(x => x.CompanyId.Equals(entity.CompanyId) && x.IsDefault == true, dataFilter);
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

    private async Task ApplyOnSavedBlAsync(Branch entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnUpdatingBlAsync(Branch existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
        var defaultEntity = await Repo.BranchRepo.SingleOrDefaultQueryableAsync(x => !x.CompanyId.Equals(existingEntity.Id) && x.IsDefault == true, dataFilter);
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

    private async Task ApplyOnUpdatedBlAsync(Branch existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivingBlAsync(Branch existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivedBlAsync(Branch existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(Branch existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(Branch existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(Branch entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(BranchFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(BranchFilterModel filter, List<Expression<Func<Branch, bool>>> predicates, List<Expression<Func<Branch, object>>> includePredicates, DataFilter dataFilter)
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