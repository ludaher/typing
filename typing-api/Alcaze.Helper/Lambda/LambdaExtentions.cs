using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Lambda
{
    public static class LambdaExtentions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, string propertyName, ComparisonOperator comparison, object value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            Expression propertyExpression = Expression.Property(parameter, propertyName);
            Type propertyType = typeof(T).GetProperty(propertyName).PropertyType;
            Expression comparisionExpression = null;
            Expression someValue = Expression.Constant(value.ChangeType(propertyType), propertyType);
            //var someValue = Expression.Constant(value, value.GetType());
            if (IsNullableType(propertyExpression.Type))
                propertyExpression = Expression.Convert(propertyExpression, someValue.Type);
            switch (comparison)
            {
                case ComparisonOperator.Contains:
                    {
                        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        comparisionExpression = Expression.Call(propertyExpression, method, someValue);
                        break;
                    }
                default:
                    {
                        comparisionExpression = Expression.MakeBinary(((ExpressionType)comparison), propertyExpression, someValue);
                        ///Este código funciona para comparación de mayor y menor en los strings
                        ///eliminar comentarios de ser necesario
                        ///
                        //method = value.GetType().GetMethod("CompareTo", new[] { value.GetType() });
                        //var result = Expression.Call(propertyExpression, method, someValue);
                        //var zero = Expression.Constant(0);
                        //comparisionExpression = Expression.MakeBinary(((ExpressionType)comparison), result, zero);
                        break;
                    }
            }
            return query.Where(Expression.Lambda<Func<T, bool>>(comparisionExpression, parameter));
        }

        public static IQueryable<T> WhereStringContains<T>(this IQueryable<T> query, string propertyName, string contains)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(contains, typeof(string));
            var containsExpression = Expression.Call(propertyExpression, method, someValue);

            return query.Where(Expression.Lambda<Func<T, bool>>(containsExpression, parameter));
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName)
        {
            var propertyType = typeof(T).GetProperty(propertyName).PropertyType;
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(propertyExpression, new[] { parameter });

            return typeof(Queryable).GetMethods()
                                    .Where(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                                    .Single()
                                    .MakeGenericMethod(new[] { typeof(T), propertyType })
                                    .Invoke(null, new object[] { query, lambda }) as IOrderedQueryable<T>;
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName)
        {
            var propertyType = typeof(T).GetProperty(propertyName).PropertyType;
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(propertyExpression, new[] { parameter });

            return typeof(Queryable).GetMethods()
                                    .Where(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2)
                                    .Single()
                                    .MakeGenericMethod(new[] { typeof(T), propertyType })
                                    .Invoke(null, new object[] { query, lambda }) as IOrderedQueryable<T>;
        }

        public static IEnumerable<T> PropertyEquals<T>(this IEnumerable<T> source, string propertyName, object propertyValue)
        {
            return (IEnumerable<T>)source.AsQueryable().PropertyEquals(propertyName, propertyValue);
        }

        public static object ChangeType(this object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
