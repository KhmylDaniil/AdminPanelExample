using AdminPanel.Core.Exceptions;
using System.Linq.Expressions;
using System.Reflection;

namespace AdminPanel.Core.Helpers
{
    public static class OrderByHelper
    {
        /// <summary>
        /// Overridden OrderBy
        /// </summary>
        /// <typeparam name="TSource">Type of entity</typeparam>
        /// <param name="query">Query</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="isAscending">is sorting ascending</param>
        /// <returns>sorted query</returns>
        public static IQueryable<TSource> OrderBy<TSource>(
            this IQueryable<TSource> query, string propertyName, bool isAscending = true)
        {
            if (query == null || propertyName == null)
                return query;

            var entityType = typeof(TSource);

            //Create x=>x.PropName
            var propertyInfo = entityType.GetProperty(propertyName)
                ?? throw new ApplicationSystemException("Incorrect sorting property.");

            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, propertyName);
            var selector = Expression.Lambda(property, arg);

            //Get System.Linq.Queryable.OrderBy() method.
            var enumarableType = typeof(Queryable);
            var metodName = isAscending ? "OrderBy" : "OrderByDescending";
            var method = enumarableType.GetMethods()
                .Where(m => m.Name == metodName && m.IsGenericMethodDefinition)
                .Single(m =>
                {
                    var parameters = m.GetParameters().ToList();
                    //Put more restriction here to ensure selecting the right overload                
                    return parameters.Count == 2;//overload that has 2 parameters
                });

            //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            MethodInfo genericMethod = method
                .MakeGenericMethod(entityType, propertyInfo.PropertyType);
            
            /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
			Note that we pass the selector as Expression to the method and we don't compile it.
			By doing so EF can extract "order by" columns and generate SQL for it.*/
            var newQuery = (IOrderedQueryable<TSource>)genericMethod
                .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }
    }
}
