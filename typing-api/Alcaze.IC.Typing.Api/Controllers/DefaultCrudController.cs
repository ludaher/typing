using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Alcaze.API;
using Alcaze.API.Factory;
using Alcaze.Helper.Log;
using Alcaze.Helper.Exceptions;
using Alcaze.IC.Typing.DTO.PersistenceEntities;
using Alcaze.IC.Typing.Api.Filters;

namespace Alcaze.IC.Typing.Api.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [JsonExceptionFilter]
    public class DefaultCrudController<T> : Controller
    where T : BaseEntity
    {
        #region API methods
        [HttpGet]
        public virtual async Task<FindResult<T>> Get(string filter = "", string sortBy = "", int page = 1, int itemsByPage = 50,  bool descending = false)
        {
            try
            {
                var conditions = new Conditions();
                conditions.AddFilter(filter);
                using (var manager = CrudManagerFactory.GetCrudManager<T>())
                {
                    var result = await manager.FindAsync(conditions, page, itemsByPage, sortBy, descending);
                    return result;
                }
            }
            catch (Exception ex)
            {
                await Logger.ErrorAsync($"Error obteniendo elementos de {typeof(T).Name}", ex);
                throw;
            }
        }

        [HttpGet]
        [Route("select/{select}")]
        public virtual async Task<FindResult<object>> Get(string select, string filter = "", string sortBy = "", int page = 1, int itemsByPage = 50,  bool descending = false)
        {
            if (select == null)
                throw new LogicException("El parámetro select es requerido.");
            try
            {
                var conditions = new Conditions();
                conditions.AddFilter(filter);
                using (var manager = CrudManagerFactory.GetCrudManager<T>())
                {
                    var result = await manager.FindSelectAsync(conditions, select, page, itemsByPage, sortBy, descending);
                    return result;
                }
            }
            catch (Exception ex)
            {
                await Logger.ErrorAsync($"Error obteniendo elementos de {typeof(T).Name}", ex);
                throw;
            }
        }

        [HttpPost]
        public virtual async Task<T> Post([FromBody]T value)
        {
            try
            {
                using (var manager = CrudManagerFactory.GetCrudManager<T>())
                {
                    value.ModifiedBy = User.Identity.Name;
                    value.ModifiedOn = DateTime.Now;
                    return await manager.UpdateAsync(value);
                }
                //return Ok("La entidad se almacenó correctamente");
            }
            catch (Exception ex)
            {
                await Logger.ErrorAsync($"Error actualizando elementos de {typeof(T).Name}", ex);
                throw;
            }
        }

        [HttpPut]
        public virtual async Task<T> Put([FromBody]T value)
        {
            try
            {
                using (var manager = CrudManagerFactory.GetCrudManager<T>())
                {
                    value.CreatedBy = User.Identity.Name;
                    value.CreatedOn = DateTime.Now; ;
                    return await manager.InsertAsync(value);
                }
                //return Ok("La entidad se almacenó correctamente");
            }
            catch (Exception ex)
            {
                await Logger.ErrorAsync($"Error insertando elementos de {typeof(T).Name}", ex);
                throw;
            }
        }

        [HttpDelete]
        public virtual async Task<ObjectResult> Delete([FromBody]T value)
        {
            try
            {
                using (var manager = CrudManagerFactory.GetCrudManager<T>())
                {
                    await manager.DeleteAsync(value);
                }
                return Ok("La entidad se eliminó correctamente");
            }
            catch (Exception ex)
            {
                await Logger.ErrorAsync($"Error eliminado elementos de {typeof(T).Name}", ex);
                throw;
            }
        }


        #endregion

        #region protected methods

        protected async Task<FindResult<T>> _Get(Conditions conditions, int page = 0, int itemsByPage = 0, string sortBy = "", bool descending = false)
        {
            using (var manager = CrudManagerFactory.GetCrudManager<T>())
            {
                var result = await manager.FindAsync(conditions, page, itemsByPage, sortBy, descending);
                return result;
            }
        }

        #endregion
        
    }
}