using Alcaze.Helper.Exceptions;
using Alcaze.Helper.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.API.Factory
{
    public static class ProcessManagerFactory
    {
        /// <summary>
        /// Nombre del ensamblado en el que se encuentran las implementacione del crud
        /// Es importante que los namespace de las implementaciones comiencen por el nombre del ensamblado
        /// </summary>
        public static string PROCESSOR_NAMESPACE = null;

        /// <summary>
        /// Nombre del ensamblado en donde se encuentran las entidades que se van administrar
        /// Es importante que los namespace de las entidades comiencen por el nombre del ensamblado
        /// </summary>
        public static string ENTITIES_NAMESPACE = null;

        private static string PROCESSOR_CLASS_SUFFIX = "Process";

        public static IProcess<Entity, ReturnEntity> GetProcessManager<Entity, ReturnEntity>()
        {
            if (string.IsNullOrWhiteSpace(PROCESSOR_NAMESPACE))
                throw new ImplementationException("No se ha configurado el PROCESSOR_NAMESPACE en el ProcessManagerFactory");
            if (string.IsNullOrWhiteSpace(ENTITIES_NAMESPACE))
                throw new ImplementationException("No se ha configurado el ENTITIES_NAMESPACE en el CudManagerFactory");

            var processor = new ProcessManager<Entity, ReturnEntity>(_MakeProcess<Entity, ReturnEntity>(typeof(Entity)));
            return processor;
        }

        private static IProcess<Entity, ReturnEntity> _MakeProcess<Entity, ReturnEntity>(Type type)
        {
            var name = type.FullName.Replace($"{ENTITIES_NAMESPACE}.", "");
            var returnName = typeof(ReturnEntity).Name;
            var className = $"{name}_{returnName}_{PROCESSOR_CLASS_SUFFIX}";
            try
            {
                var processFullName = $"{PROCESSOR_NAMESPACE}.{className}";
                Assembly assembly = Assembly.Load(PROCESSOR_NAMESPACE);
                //var processType = Type.GetType(processFullName);
                var processType = assembly.GetType(processFullName);
                if (processType == null)
                    throw new Exception($"No se encontró el proceso para {type.Name}");
                var process = Activator.CreateInstance(processType, new object[0]);
                return (IProcess<Entity, ReturnEntity>)process;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error creando el procesador para la entidad {type.Name}", ex);
                throw new Exception($"Error creando el procesador para la entidad {type.Name}", ex);
            }
        }

        public static String GetAssemblyNameContainingType(String typeName)
        {
            foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type t = currentassembly.GetType(typeName, false, true);
                if (t != null) { return currentassembly.FullName; }
            }

            return "not found";
        }

    }
}
