namespace TH.RepoSQL
{
    public interface IRepo<TEntity> where TEntity : class
    {
        Task<TEntity?> Save(TEntity entity);
        Task SaveRange(IEnumerable<TEntity> entities);
        Task<TEntity?> FindById(string id);
    }
}