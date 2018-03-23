using Alcaze.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.API
{
    public interface ICrudValidator<Entity>:IDisposable
    {
        #region métodos sincronos
        /// <summary>
        /// Realiza validaciones previas a la busqueda
        /// </summary>
        /// <param name="searchConditions"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        void Find(List<Tuple<string, ComparisonOperator, object>> searchConditions, int page, int pageSize, string orderBy, bool ascending = true);

        /// <summary>
        /// Realiza validaciones previas a la inserción de la entidad 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Entidad creada</returns>
        void Insert(Entity entity);

        /// <summary>
        /// Realiza validaciones previas a la inserción de las entidades
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Lista de entidades creadas</returns>
        void Insert(List<Entity> entity);

        /// <summary>
        /// Realiza validaciones previas a la actualización de la entidad
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Entidad actualizada</returns>
        void Update(Entity entity);

        /// <summary>
        /// Validaciones previas a la eliminación de la entidad
        /// </summary>
        /// <param name="entity"></param>
        void Delete(Entity entity);
        #endregion

        #region métodos asincronos
        /// <summary>
        /// Realiza validaciones previas a la busqueda
        /// </summary>
        /// <param name="searchConditions"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        Task FindAsync(List<Tuple<string, ComparisonOperator, object>> searchConditions, int page, int pageSize, string orderBy, bool ascending = true);

        /// <summary>
        /// Realiza validaciones previas a la inserción de la entidad 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Entidad creada</returns>
        Task InsertAsync(Entity entity);

        /// <summary>
        /// Realiza validaciones previas a la inserción de las entidades
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Lista de entidades creadas</returns>
        Task InsertAsync(List<Entity> entity);

        /// <summary>
        /// Realiza validaciones previas a la actualización de la entidad
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Entidad actualizada</returns>
        Task UpdateAsync(Entity entity);

        /// <summary>
        /// Validaciones previas a la eliminación de la entidad
        /// </summary>
        /// <param name="entity"></param>
        Task DeleteAsync(Entity entity);

        /// <summary>
        /// Realiza validaciones previas a la actualización de la entidad
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Entidad actualizada</returns>
        Task UpdateAsync(List<Entity> entity);

        #endregion
    }
}
