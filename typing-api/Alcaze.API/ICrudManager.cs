using Alcaze.APIr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.API
{
    public interface ICrudManager<Entity>: IDisposable
    {

        /// <summary>
        /// Obtiene lista de entidad basado en las condiciones de busqueda enviadas
        /// </summary>
        /// <param name="searchConditions">Condiciones de busqueda</param>
        /// <param name="page">Página actual (0 = sin paginación)</param>
        /// <param name="pageSize">Número de elementos por página (0 = sin paginación)</param>
        /// <param name="orderBy">Nombre de la propiedad por la que se quiere ordenar la información</param>
        /// <param name="descending">Dirección de ordenamiento, ascendente o descendente</param>
        /// <returns></returns>
        FindResult<Entity> Find(Conditions searchConditions, int page, int pageSize, string orderBy, bool descending = true, List<string> includes = null);


        /// <summary>
        /// Obtiene lista de entidad basado en las condiciones de busqueda enviadas
        /// </summary>
        /// <param name="searchConditions">Condiciones de busqueda</param>
        /// <param name="selectFields">Campos que se desean consultar</param>
        /// <param name="page">Página actual (0 = sin paginación)</param>
        /// <param name="pageSize">Número de elementos por página (0 = sin paginación)</param>
        /// <param name="orderBy">Nombre de la propiedad por la que se quiere ordenar la información</param>
        /// <param name="descending">Dirección de ordenamiento, ascendente o descendente</param>
        /// <returns></returns>
        FindResult<dynamic> FindSelect(Conditions searchConditions, string selectFields, int page, int pageSize, string orderBy, bool descending = true);


        /// <summary>
        /// Ingresa una entidad en la base de datos
        /// </summary>
        /// <param name="entity">Entidad a insertar</param>
        /// <returns>Entidad creada</returns>
        Entity Insert(Entity entity);

        /// <summary>
        /// Ingresa una lista de entidades en base de datos
        /// </summary>
        /// <param name="entities">Lista de entidades a insertar</param>
        /// <returns>Lista de entidades creadas</returns>
        List<Entity> Insert(List<Entity> entities);

        /// <summary>
        /// Actualiza una entidad realizando la busqueda por sus llaves primarias
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Entidad actualizada</returns>
        Entity Update(Entity entity);

        /// <summary>
        /// Elimina un registro buscando por las llaves primarias
        /// </summary>
        /// <param name="entity">Entidad eliminada</param>
        void Delete(Entity entity);

        /// <summary>
        /// Obtiene lista de entidad basado en las condiciones de busqueda enviadas de forma asíncrona
        /// </summary>
        /// <param name="searchConditions">Condiciones de busqueda</param>
        /// <param name="page">Página actual (0 = sin paginación)</param>
        /// <param name="pageSize">Número de elementos por página (0 = sin paginación)</param>
        /// <param name="orderBy">Nombre de la propiedad por la que se quiere ordenar la información</param>
        /// <param name="descending">Dirección de ordenamiento, ascendente o descendente</param>
        /// <returns></returns>
        Task<FindResult<Entity>> FindAsync(Conditions searchConditions, int page, int pageSize, string orderBy, bool descending = true, List<string> includes = null);

        /// <summary>
        /// Obtiene lista de entidad basado en las condiciones de busqueda enviadas de forma asíncrona
        /// </summary>
        /// <param name="searchConditions">Condiciones de busqueda</param>
        /// <param name="selectFields">Campos por los que se desea filtrar</param>
        /// <param name="page">Página actual (0 = sin paginación)</param>
        /// <param name="pageSize">Número de elementos por página (0 = sin paginación)</param>
        /// <param name="orderBy">Nombre de la propiedad por la que se quiere ordenar la información</param>
        /// <param name="descending">Dirección de ordenamiento, ascendente o descendente</param>
        /// <returns></returns>
        Task<FindResult<dynamic>> FindSelectAsync(Conditions searchConditions, string selectFields, int page, int pageSize, string orderBy, bool descending = true);


        /// <summary>
        /// Ingresa una entidad en la base de datos de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a insertar</param>
        /// <returns>Entidad creada</returns>
        Task<Entity> InsertAsync(Entity entity);

        /// <summary>
        /// Ingresa una lista de entidades en base de datos de forma asíncrona
        /// </summary>
        /// <param name="entities">Lista de entidades a insertar</param>
        /// <returns>Lista de entidades creadas</returns>
        Task<List<Entity>> InsertAsync(List<Entity> entities);

        /// <summary>
        /// Actualiza una entidad realizando la busqueda por sus llaves primarias de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Entidad actualizada</returns>
        Task<Entity> UpdateAsync(Entity entity);

        /// <summary>
        /// Elimina un registro buscando por las llaves primarias de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad eliminada</param>
        Task DeleteAsync(Entity entity);

        /// <summary>
        /// Elimina un registro buscando por condiciones
        /// </summary>
        /// <param name="searchConditions">Condiciones de filtro</param>
        /// <returns></returns>
        Task DeleteAsync(Conditions searchConditions);

        /// <summary>
        /// Retorna un boleano que indica si existe el elemento
        /// basado en las condiciones de busqueda enviadas
        /// </summary>
        /// <param name="searchConditions">Condiciones de busqueda</param>
        /// <returns>Exsisten elementos basados en las condiciones</returns>
        bool Any(Conditions searchConditions);

        /// <summary>
        /// Retorna un boleano que indica si existe el elemento
        /// basado en las condiciones de busqueda enviadas
        /// </summary>
        /// <param name="searchConditions">Condiciones de busqueda</param>
        /// <returns>Exsisten elementos basados en las condiciones</returns>
        Task<bool> AnyAsync(Conditions searchConditions);

        /// <summary>
        /// Actualiza una entidad realizando la busqueda por sus llaves primarias de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Entidad actualizada</returns>
        Task<List<Entity>> UpdateAsync(List<Entity> entity); 
    }
}
