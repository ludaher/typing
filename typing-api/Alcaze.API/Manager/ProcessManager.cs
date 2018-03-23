using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.API
{
    internal class ProcessManager<Entity, ReturnEntity> : IProcess<Entity, ReturnEntity>
    {
        private IProcess<Entity,ReturnEntity> _processManager;
        /// <summary>
        /// Constructor del gestor de procesos
        /// </summary>
        /// <param name="processManager">Procesador específico</param>
        public ProcessManager(IProcess<Entity, ReturnEntity> processManager)
        {
            this._processManager = processManager;
        }
        /// <summary>
        /// Ejecuta dispose del procesador específico
        /// </summary>
        public void Dispose()
        {
            _processManager.Dispose();
        }
        /// <summary>
        /// Ejecuta el proceso
        /// </summary>
        /// <param name="entity">Entidad con la que se ejecutará el proceso</param>
        /// <returns>Entidad de respuesta</returns>
        public async Task<ReturnEntity> Execute(Entity entity)
        {
            return await _processManager.Execute(entity);
        }
    }
}
