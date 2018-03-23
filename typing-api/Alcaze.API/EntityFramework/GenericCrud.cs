using Alcaze.APIr;
using Alcaze.Helper.Lambda;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using Alcaze.Helper;
using Alcaze.Helper.Exceptions;

namespace Alcaze.API.EntityFramework
{
    /// <summary>
    /// Clase encargada de realizar las operaciones CRUD de una entidad de negocio
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class GenericCrud<Entity, CustomContext> : ICrudManager<Entity>
        where Entity : class
        where CustomContext : DbContext
    {
        protected CustomContext _context;

        public GenericCrud(CustomContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Sinchronous methods

        public virtual FindResult<Entity> Find(Conditions searchConditions, int page, int pageSize, string orderBy, bool descending = true, List<string> includes = null)
        {
            //if (typeof(Inactivable).IsAssignableFrom(typeof(Entity)))
            //{
            //    if (searchConditions == null)
            //        searchConditions = new Conditions();
            //    searchConditions.AddCondition("Inactive", ComparisonOperator.Equal, false);
            //}
            var result = new FindResult<Entity>();
            int total;
            IQueryable<Entity> query = _Find(searchConditions, page, pageSize, orderBy, descending, includes, out total);
            result.Total = total;
            //query.Select()
            result.ResultList = query.ToList();
            return result;
        }


        public virtual FindResult<dynamic> FindSelect(Conditions searchConditions, string selectFields, int page, int pageSize, string orderBy, bool descending = true)
        {
            //if (typeof(Inactivable).IsAssignableFrom(typeof(Entity)))
            //{
            //    if (searchConditions == null)
            //        searchConditions = new Conditions();
            //    searchConditions.AddCondition("Inactive", ComparisonOperator.Equal, false);
            //}
            var result = new FindResult<object>();
            int total;
            IQueryable<Entity> query = _Find(searchConditions, page, pageSize, orderBy, descending, null, out total);
            result.Total = total;
            var newSelect = query.Select(_CreateNewStatement(selectFields));
            result.ResultList = newSelect.ToList();

            return result;
        }

        public virtual List<Entity> Insert(List<Entity> entities)
        {
            foreach (var entity in entities)
                _context.Set<Entity>().Add(entity);
            _context.SaveChanges();
            return entities;
        }

        public virtual Entity Insert(Entity entity)
        {
            _context.Set<Entity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual Entity Update(Entity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public virtual void Delete(Entity entity)
        {
            //if (entity is Inactivable)
            //{
            //    var prop = entity.GetType().GetProperty("Inactive", BindingFlags.Public | BindingFlags.Instance);
            //    if (prop != null && prop.CanWrite)
            //        prop.SetValue(entity, true, null);
            //    _context.Entry(entity).State = EntityState.Modified;
            //}
            //else
            {
                _context.Set<Entity>().Remove(entity);
            }
            _context.SaveChanges();
        }
        #endregion

        #region Asynchronous methods

        public virtual async Task<FindResult<Entity>> FindAsync(Conditions searchConditions, int page, int pageSize, string orderBy, bool descending = true, List<string> includes = null)
        {
            //if (typeof(Inactivable).IsAssignableFrom(typeof(Entity)))
            //{
            //    if (searchConditions == null)
            //        searchConditions = new Conditions();
            //    searchConditions.AddCondition("Inactive", ComparisonOperator.Equal, false);
            //}
            var result = new FindResult<Entity>();
            int total;
            IQueryable<Entity> query = _Find(searchConditions, page, pageSize, orderBy, descending, includes, out total);
            //query.Select(_CreateNewStatement(selectFields))
            result.Total = total;
            result.ResultList = await query.ToListAsync();
            return result;
        }

        public async Task<FindResult<dynamic>> FindSelectAsync(Conditions searchConditions, string selectFields, int page, int pageSize, string orderBy, bool descending = true)
        {
            var result = new FindResult<object>();
            int total;
            IQueryable<Entity> query = _Find(searchConditions, page, pageSize, orderBy, descending, null, out total);
            //var newSelect = query.Select(_CreateNewStatement(selectFields));
            var selector = $"new({selectFields})";
            var newSelect = query.Select(selector);
            result.Total = total;
            result.ResultList = await newSelect.ToDynamicListAsync();
            return result;
        }
        public virtual async Task<List<Entity>> InsertAsync(List<Entity> entities)
        {
            foreach (var entity in entities)
                _context.Set<Entity>().Add(entity);
            await _context.SaveChangesAsync();
            return entities;
        }

        public virtual async Task<Entity> InsertAsync(Entity entity)
        {
            _context.Set<Entity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<Entity> UpdateAsync(Entity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            var save = await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            //if (entity is Inactivable)
            //{
            //    var prop = entity.GetType().GetProperty("Inactive", BindingFlags.Public | BindingFlags.Instance);
            //    if (prop != null && prop.CanWrite)
            //        prop.SetValue(entity, true, null);
            //    _context.Entry(entity).State = EntityState.Modified;
            //}
            //else
            {
                _context.Set<Entity>().Remove(entity);
            }
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Conditions searchConditions)
        {
            int total;
            var query = _Find(searchConditions, 0, 0, "", false, null, out total);
            {
                _context.RemoveRange(query);
            }
            await _context.SaveChangesAsync();
        }

        public bool Any(Conditions searchConditions)
        {
            //if (typeof(Inactivable).IsAssignableFrom(typeof(Entity)))
            //{
            //    if (searchConditions == null)
            //        searchConditions = new Conditions();
            //    searchConditions.AddCondition("Inactive", ComparisonOperator.Equal, false);
            //}
            IQueryable<Entity> query = _context.Set<Entity>();
            if (searchConditions != null)
                foreach (var condition in searchConditions)
                    query = query.Where(condition.Item1, condition.Item2, condition.Item3);
            return query.Any();
        }

        public async Task<bool> AnyAsync(Conditions searchConditions)
        {
            //if (typeof(Inactivable).IsAssignableFrom(typeof(Entity)))
            //{
            //    if (searchConditions == null)
            //        searchConditions = new Conditions();
            //    searchConditions.AddCondition("Inactive", ComparisonOperator.Equal, false);
            //}
            IQueryable<Entity> query = _context.Set<Entity>();
            if (searchConditions != null)
                foreach (var condition in searchConditions)
                    query = query.Where(condition.Item1, condition.Item2, condition.Item3);
            return await query.AnyAsync();
        }

        public virtual async Task<List<Entity>> UpdateAsync(List<Entity> entities)
        {
            foreach (var entity in entities)
                _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entities;
        }

        #endregion


        #region private methods
        public static string GetKeyField()
        {
            var type = typeof(Entity);
            var allProperties = type.GetProperties();
            var keyProperty = allProperties.SingleOrDefault(p => p.IsDefined(typeof(KeyAttribute)));
            return keyProperty != null ? keyProperty.Name : null;
        }

        private Func<Entity, Entity> _CreateNewStatement(string fields)
        {
            var type = typeof(Entity);
            // input parameter "o"
            var xParameter = Expression.Parameter(type, "o");

            // new statement "new Data()"
            var xNew = Expression.New(type);

            // create initializers
            var bindings = fields.Split(',').Select(o => o.Trim())
                .Select(o =>
                {

                    // property "Field1"
                    var mi = typeof(Entity).GetProperty(o);

                    // original value "o.Field1"
                    var xOriginal = Expression.Property(xParameter, mi);

                    // set value "Field1 = o.Field1"
                    return Expression.Bind(mi, xOriginal);


                }
            );

            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<Entity, Entity>>(xInit, xParameter);

            // compile to Func<Data, Data>
            return lambda.Compile();
        }


        private IQueryable<Entity> _Find(Conditions searchConditions, int page, int pageSize, string orderBy, bool descending, List<string> includes, out int total)
        {
            IQueryable<Entity> query = _context.Set<Entity>()
               .AsNoTracking();
            ///Apply conditions
            ///
            if (searchConditions != null)
                foreach (var condition in searchConditions)
                    query = query.Where(condition.Item1, condition.Item2, condition.Item3);
            total = query.Count();
            ///Apply order
            ///
            if (string.IsNullOrWhiteSpace(orderBy))
                orderBy = GetKeyField();
            if (orderBy == null)
                throw new LogicException("El campo de orden es obligatorio.");
            if (descending)
                query = query.OrderByDescending(orderBy);
            else
                query = query.OrderBy(orderBy);
            ///Apply pagining
            ///
            if (pageSize > 0)
                query = query.Skip((page - 1) * pageSize).Take(pageSize);
            ///Include another entities
            ///
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query;
        }


        #endregion
    }
}
