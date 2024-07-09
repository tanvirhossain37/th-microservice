namespace TH.Repo;

public interface ICustomSort
{
    IOrderedQueryable<T> OrderBy<T>(IQueryable<T> source, string propertyName) where T : class;
    IOrderedQueryable<T> OrderByDescending<T>(IQueryable<T> source, string propertyName) where T : class;
    IOrderedQueryable<T> ThenBy<T>(IOrderedQueryable<T> source, string propertyName) where T : class;
    IOrderedQueryable<T> ThenByDescending<T>(IOrderedQueryable<T> source, string propertyName) where T : class;
}