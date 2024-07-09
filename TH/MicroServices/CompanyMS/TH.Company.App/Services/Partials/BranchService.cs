using System.Linq.Expressions;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class BranchService
{
    //Add additional services if any

    //private BranchService(IUow repo, IBranchUserService branchUserService) : this()
    //{
    //}

    private void ApplyOnSavingBl(Branch entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnSavedBl(Branch entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnUpdatingBl(Branch existingEntity, DataFilter dataFilter)
    {
        try
        {
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnUpdatedBl(Branch existingEntity, DataFilter dataFilter)
    {
        try
        {
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnDeletingBl(Branch existingEntity, DataFilter dataFilter)
    {
        try
        {
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnDeletedBl(Branch existingEntity, DataFilter dataFilter)
    {
        try
        {
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnFindByIdBl(Branch entity, DataFilter dataFilter)
    {
        try
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyOnGetBl(BranchFilterModel filter, DataFilter dataFilter)
    {
        try
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            //todo
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ApplyCustomGetFilterBl(BranchFilterModel filter, List<Expression<Func<Branch, bool>>> predicates)
    {
        try
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
        catch (Exception)
        {
            throw;
        }
    }

    private void DisposeOthers()
    {
        try
        {
        }
        catch (Exception)
        {
            throw;
        }
    }
}