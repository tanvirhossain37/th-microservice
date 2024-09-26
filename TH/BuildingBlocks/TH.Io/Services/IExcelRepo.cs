using System.Linq.Expressions;

namespace TH.Io
{
    public interface IExcelRepo
    {
        IEnumerable<TEntity> Fetch<TEntity>(string path) where TEntity : class, new();
        IEnumerable<TEntity> FetchMapColumnIndex<TEntity>(string path) where TEntity : class, new();

        //, new() - it needs concrete class
        CustomFile Save<TEntity>(IEnumerable<TEntity> entities, string name, bool xlsx = true, IList<Expression<Func<TEntity, object>>> ignores = null) where TEntity : class;
    }
}
