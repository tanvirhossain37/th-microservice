using System.Linq.Expressions;

namespace TH.Repo;

public class CustomSort : ICustomSort
{
    public IOrderedQueryable<T> OrderBy<T>(IQueryable<T> source, string propertyName) where T : class
    {
        return OrderingHelper(source, propertyName, false, false);
    }

    public IOrderedQueryable<T> OrderByDescending<T>(IQueryable<T> source, string propertyName) where T : class
    {
        return OrderingHelper(source, propertyName, true, false);
    }

    public IOrderedQueryable<T> ThenBy<T>(IOrderedQueryable<T> source, string propertyName) where T : class
    {
        return OrderingHelper(source, propertyName, false, true);
    }

    public IOrderedQueryable<T> ThenByDescending<T>(IOrderedQueryable<T> source, string propertyName) where T : class
    {
        return OrderingHelper(source, propertyName, true, true);
    }

    private IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
    {
        try
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty); // I don't care about some naming
            //MemberExpression property = Expression.PropertyOrField(param, propertyName);

            //Tanvir - The method below gives ability to sort parent [e.Employee.FirstName]
            MemberExpression property = GetMemberExpression(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty), new[] { typeof(T), property.Type },
                source.Expression, Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private MemberExpression GetMemberExpression(Expression param, string propertyName)
    {
        try
        {
            if (propertyName.Contains("."))
            {
                int index = propertyName.IndexOf(".");
                var subParam = Expression.Property(param, propertyName.Substring(0, index));
                return GetMemberExpression(subParam, propertyName.Substring(index + 1));
            }

            return Expression.Property(param, propertyName);
        }
        catch (Exception)
        {
            throw;
        }
    }
}