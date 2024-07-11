using System.Linq.Expressions;
using TH.Common.Lang;
using TH.Common.Model;
using TH.Common.Util;
using TH.ShadowMS.Core;

namespace TH.ShadowMS.App.Services;

public class ShadowService : IShadowService
{
    protected readonly IShadowRepo Repo;

    public ShadowService(IShadowRepo repo)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public async Task<Shadow> SaveAsync(Shadow entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = Util.TryGenerateGuid();
        entity.CreatedDate = DateTime.Now;

        ApplyValidationBl(entity);

        await Repo.SaveAsync(entity);

        return entity;
    }



    public async Task<Shadow> UpdateAsync(Shadow entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.FindByIdAsync(entity.Id);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        return existingEntity;
    }

    public async Task<bool> SoftDeleteAsync(Shadow entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.FindByIdAsync(entity.Id);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        existingEntity.ModifiedDate = DateTime.Now;
        existingEntity.Active = false;

        await Repo.UpdateAsync(existingEntity.Id, existingEntity);

        return true;
    }

    public async Task<bool> DeleteAsync(Shadow entity, DataFilter dataFilter, bool commit = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingEntity = await Repo.FindByIdAsync(entity.Id);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        await Repo.DeleteAsync(existingEntity.Id);

        return true;
    }

    public async Task<Shadow> FindAsync(ShadowFilterModel filter, DataFilter dataFilter)
    {
        var existingEntity = await Repo.FindByIdAsync(filter.Id);
        if (existingEntity == null) throw new CustomException(Lang.Find("error_notfound"));

        return existingEntity;
    }

    public async Task<IEnumerable<Shadow>> GetAsync(ShadowFilterModel filter, DataFilter dataFilter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        if (filter.SortFilters == null) filter.SortFilters = new List<SortFilter>();
        if (filter.SortFilters.Count <= 0) filter.SortFilters.Add(new SortFilter { PropertyName = "Id", Operation = OrderByEnum.Ascending });

        var predicates = new List<Expression<Func<Shadow, bool>>>();

        #region Filters

        if (!string.IsNullOrWhiteSpace(filter.Id)) predicates.Add(t => t.Id.Contains(filter.Id.Trim()));
        if (filter.CreatedDate.HasValue) predicates.Add(t => t.CreatedDate == filter.CreatedDate);
        if (filter.ModifiedDate.HasValue) predicates.Add(t => t.ModifiedDate == filter.ModifiedDate);
        if (filter.Active.HasValue) predicates.Add(t => t.Active == filter.Active);
        if (!string.IsNullOrWhiteSpace(filter.SpaceId)) predicates.Add(t => t.SpaceId.Contains(filter.SpaceId.Trim()));
        if (!string.IsNullOrWhiteSpace(filter.UserName)) predicates.Add(t => t.UserName.Contains(filter.UserName.Trim()));
        if (filter.ActivityNameEnum.HasValue) predicates.Add(t => t.ActivityName == filter.ActivityNameEnum);
        if (!string.IsNullOrWhiteSpace(filter.Message)) predicates.Add(t => t.Message.Contains(filter.Message.Trim()));

        #endregion

        var entities = await Repo.GetFilterableAsync(predicates, filter.PageIndex, filter.PageSize);
        if (!entities.Any()) throw new CustomException(Lang.Find("error_notfound"));

        return entities;
    }

    public void Dispose()
    {
        Repo?.Dispose();
    }

    #region Businiess Logic

    private void ApplyValidationBl(Shadow entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Id = string.IsNullOrWhiteSpace(entity.Id) ? throw new CustomException($"{Lang.Find("validation_error")}: Id") : entity.Id.Trim();
        if (!Util.TryIsValidDate(entity.CreatedDate)) throw new CustomException($"{Lang.Find("validation_error")}: CreatedDate");
        if (entity.ModifiedDate.HasValue)
        {
            if (!Util.TryIsValidDate((DateTime)entity.ModifiedDate)) throw new CustomException($"{Lang.Find("validation_error")}: ModifiedDate");
        }

        entity.SpaceId = string.IsNullOrWhiteSpace(entity.SpaceId)
            ? throw new CustomException($"{Lang.Find("validation_error")}: SpaceId")
            : entity.SpaceId.Trim();
        entity.ClientId = string.IsNullOrWhiteSpace(entity.ClientId)
            ? throw new CustomException($"{Lang.Find("validation_error")}: ClientId")
            : entity.ClientId.Trim();
        entity.UserName = string.IsNullOrWhiteSpace(entity.UserName)
            ? throw new CustomException($"{Lang.Find("validation_error")}: UserName")
            : entity.UserName.Trim();
        //entity.ActivityName = (entity.ActivityName == null) ? ActivityNameEnum.Save : entity.ActivityName
        entity.Message = string.IsNullOrWhiteSpace(entity.Message) ? string.Empty : entity.Message.Trim();
    }

    #endregion
}