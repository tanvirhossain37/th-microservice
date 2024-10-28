using System.Linq.Expressions;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class UserCompanyService
{
    //Add additional services if any

    //public UserCompanyService(IUow repo) : this()
    //{
    //}

    private async Task ApplyOnSavingBlAsync(UserCompany entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnSavedBlAsync(UserCompany entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnUpdatingBlAsync(UserCompany existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnUpdatedBlAsync(UserCompany existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivingBlAsync(UserCompany existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnArchivedBlAsync(UserCompany existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletingBlAsync(UserCompany existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnDeletedBlAsync(UserCompany existingEntity, DataFilter dataFilter)
    {
        if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

        //todo
    }

    private async Task ApplyOnFindBlAsync(UserCompany entity, DataFilter dataFilter)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        //todo
    }

    private async Task ApplyOnGetBlAsync(UserCompanyFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //todo
    }

    private async Task ApplyCustomGetFilterBlAsync(UserCompanyFilterModel filter, List<Expression<Func<UserCompany, bool>>> predicates, List<Expression<Func<UserCompany, object>>> includePredicates, DataFilter dataFilter)
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

        if (!string.IsNullOrWhiteSpace(filter.UserName))
        {
            predicates.Add(t => t.User.UserName.Equals(filter.UserName.Trim()));
        }
    }

    public async Task<UserCompany> FindByCompanyIdAsync(UserCompanyFilterModel filter, DataFilter dataFilter)
    {
        if (filter is null)
        {
            throw new ArgumentNullException(nameof(filter));
        }

        var entity = await Repo.UserCompanyRepo.SingleOrDefaultQueryableAsync(x=>x.CompanyId.Equals(filter.CompanyId), dataFilter);
        if (entity == null) throw new CustomException(Lang.Find("data_notfound"));

        return entity;
    }

    private void DisposeOthers()
    {
        //todo
    }
}