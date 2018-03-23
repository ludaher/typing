using System.Collections.Generic;

namespace Alcaze.API
{
    /// <summary>
    /// Clase que contiene resultados de una busqueda de una entidad de negocio
    /// </summary>
    /// <typeparam name="EntityModel">Entidad de negocio</typeparam>
    public class FindResult<EntityModel>
    {
        public int Total { get; set; }
        public IEnumerable<EntityModel> ResultList { get; set; }
        public string Message { get; set; }
    }
}
