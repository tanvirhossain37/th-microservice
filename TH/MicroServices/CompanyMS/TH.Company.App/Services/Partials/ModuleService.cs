using System.Linq.Expressions;
using TH.Common.Model;
using TH.Common.Util;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial class ModuleService
{
    //Add additional services if any

    //private ModuleService(IUow repo, IPermissionService permissionService) : this()
    //{
    //}

    private void ApplyOnSavingBl(Module entity, DataFilter dataFilter)
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

    private void ApplyOnSavedBl(Module entity, DataFilter dataFilter)
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

    private void ApplyOnUpdatingBl(Module existingEntity, DataFilter dataFilter)
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

    private void ApplyOnUpdatedBl(Module existingEntity, DataFilter dataFilter)
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

    private void ApplyOnDeletingBl(Module existingEntity, DataFilter dataFilter)
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

    private void ApplyOnDeletedBl(Module existingEntity, DataFilter dataFilter)
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

    private void ApplyOnFindByIdBl(Module entity, DataFilter dataFilter)
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

    private void ApplyOnGetBl(ModuleFilterModel filter, DataFilter dataFilter)
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

    private void ApplyCustomGetFilterBl(ModuleFilterModel filter, List<Expression<Func<Module, bool>>> predicates)
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