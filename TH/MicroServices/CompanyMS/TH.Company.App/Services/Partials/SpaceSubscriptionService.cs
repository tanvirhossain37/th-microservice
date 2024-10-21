using System.Linq.Expressions;
using TH.Common.Lang;
using TH.CompanyMS.Core;
using TH.Common.Model;
using TH.Common.Util;

namespace TH.CompanyMS.App;

public partial class SpaceSubscriptionService
{
    //Add additional services if any

    //private SpaceSubscriptionService(IUow repo) : this()
    //{
    //}

    private async Task ApplyOnSavingBlAsync(SpaceSubscription entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
        //todo
        var spaceSubscriptions = await Repo.SpaceSubscriptionRepo.GetQueryableAsync(x => x.SpaceId == entity.SpaceId, null,
            branches => branches.OrderBy(m => m.Id),
            (int)PageEnum.PageIndex, (int)PageEnum.All, dataFilter);
        if (spaceSubscriptions is null)
        {
            entity.IsCurrent = true; //first plan
        }
        else
        {
            if (entity.IsCurrent)
            {
                foreach (var branch in spaceSubscriptions)
                {
                    branch.IsCurrent = false;
                }
            }
        }
    }

    private async Task ApplyOnSavedBlAsync(SpaceSubscription entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnUpdatingBlAsync(SpaceSubscription existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnUpdatedBlAsync(SpaceSubscription existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivingBlAsync(SpaceSubscription existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivedBlAsync(SpaceSubscription existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(SpaceSubscription existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(SpaceSubscription existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(SpaceSubscription entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(SpaceSubscriptionFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(SpaceSubscriptionFilterModel filter, List<Expression<Func<SpaceSubscription, bool>>> predicates,
        List<Expression<Func<SpaceSubscription, object>>> includePredicates, DataFilter dataFilter)
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

    public async Task<SpaceSubscription> FindBySpaceIdAsync(SpaceSubscriptionFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //current plan
        var entity = await Repo.SpaceSubscriptionRepo.SingleOrDefaultQueryableAsync(x => x.SpaceId.Equals(filter.SpaceId) && x.IsCurrent == true);
        if (entity == null) throw new CustomException(Lang.Find("data_notfound"));

        return entity;
    }
}