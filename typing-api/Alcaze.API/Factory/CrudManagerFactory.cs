using Alcaze.API.Attributes;
using Alcaze.APIr;
using Alcaze.Helper.Attributes;
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
    public class CrudManagerFactory: DependencyInjecton
    {
       
        /// <summary>
        /// Nombre del ensamblado en el que se encuentran las implementacione del crud
        /// Es importante que los namespace de las implementaciones comiencen por el nombre del ensamblado
        /// </summary>
        public static string CRUD_NAMESPACE = null;
        /// <summary>
        /// Nombre del ensamblado donde se encuentran las implementaciones de los validadores
        /// Es importante que los namespace de las implementaciones comiencen por el nombre del ensamblado
        /// </summary>
        public static string CRUD_VALIDATOR_NAMESPACE = null;
        /// <summary>
        /// Nombre del ensamblado en donde se encuentran las entidades que se van administrar
        /// Es importante que los namespace de las entidades comiencen por el nombre del ensamblado
        /// </summary>
        public static string ENTITIES_NAMESPACE = null;

        private static string CRUD_CLASS_SUFFIX = "Crud";
        private static string CRUD_VALIDATOR_CLASS_SUFFIX = "Validator";

        /// <summary>
        /// Se obtiene el administrador de CRUD de la entidad definida basada en el namespace de CrudImplementationAttribute.
        /// DefaultCrud debe ser el nombre de la implementación por defecto
        /// </summary>
        /// <typeparam name="Entity">Entidad qie se desea procesar</typeparam>
        /// <returns></returns>
        public static ICrudManager<Entity> GetCrudManager<Entity>()
            where Entity : class
        {
            if (string.IsNullOrWhiteSpace(CRUD_NAMESPACE))
                throw new ImplementationException("No se ha configurado el CRUD_NAMESPACE en el CudManagerFactory");
            if (string.IsNullOrWhiteSpace(CRUD_VALIDATOR_NAMESPACE))
                throw new ImplementationException("No se ha configurado el CRUD_VALIDATOR_NAMESPACE en el CudManagerFactory");
            if (string.IsNullOrWhiteSpace(ENTITIES_NAMESPACE))
                throw new ImplementationException("No se ha configurado el ENTITIES_NAMESPACE en el CudManagerFactory");

            var crudManager = new CrudManager<Entity>(_BuildCrudManager<Entity>());
            crudManager.CrudValidator = _BuildCrudValidator<Entity>();
            return crudManager;
        }

        /// <summary>
        /// Construye el administrador de CRUD
        /// </summary>
        /// <typeparam name="Entity">Entidad que se administra</typeparam>
        /// <returns>Implementación del ICrudManager</returns>
        private static ICrudManager<Entity> _BuildCrudManager<Entity>()
            where Entity : class
        {
            var type = typeof(Entity);
            var name = type.FullName.Replace($"{ENTITIES_NAMESPACE}.", "");
            var className = $"{name}_{CRUD_CLASS_SUFFIX}";
            try
            {
                var assembly = Assembly.Load(CRUD_NAMESPACE);
                var crudFullName = $"{CRUD_NAMESPACE}.{className}";
                var crudType = Type.GetType($"{CRUD_NAMESPACE}.{className}, {CRUD_NAMESPACE}");
                if (crudType == null)
                {
                    var defaultType = Type.GetType($"{CRUD_NAMESPACE}.DefaultCrud`1, {CRUD_NAMESPACE}");
                    if (defaultType == null)
                        throw new ImplementationException($"No se encontró clase DefaultCrud<T> que es una implementación por defecto del crud.");
                    Type[] typeArgs = { typeof(Entity) };
                    crudType = defaultType.MakeGenericType(typeArgs);
                }
                if (crudType == null)
                    throw new ImplementationException($"No se encontró el administrador del crud para {type.Name}");
                var createInstance = ObjectInstantiater(crudType);
                var crudManager =  createInstance() as ICrudManager<Entity>;
                if (crudManager == null)
                    throw new ImplementationException($"No se encontró el administrador del crud para {type.Name}");
                return crudManager;
            }
            catch (ImplementationException) { throw; }
            catch (Exception ex)
            {
                Logger.Error($"Error creando el procesador para la entidad {type.Name}", ex);
                throw new Exception($"Error creando el procesador para la entidad {type.Name}", ex);
            }
        }


        /// <summary>
        /// Construye el VALIDATOR de la entidad basado en el atributo CrudImplementationAttribute
        /// </summary>
        /// <typeparam name="Entity">Entidad que se administra</typeparam>
        /// <returns>Validador de la entidad, si exsiste</returns>
        private static ICrudValidator<Entity> _BuildCrudValidator<Entity>() where Entity : class
        {
            var type = typeof(Entity);
            var name = type.FullName.Replace($"{ENTITIES_NAMESPACE}.", "");
            var className = $"{name}_{CRUD_VALIDATOR_CLASS_SUFFIX}";
            var crudFullName = $"{CRUD_VALIDATOR_NAMESPACE}.{className}";
            var crudValidatorType = Type.GetType(crudFullName + ", " + CRUD_VALIDATOR_NAMESPACE);
            if (crudValidatorType == null)
                return null;
            var crudValidator = Activator.CreateInstance(crudValidatorType);
            if (crudValidator == null)
                return null;
            return (ICrudValidator<Entity>)crudValidator;
        }

    }
}
