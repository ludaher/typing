using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Alcaze.API.Factory
{
    public class DependencyInjecton
    {
        // Delegate for holding object instantiator method
        public delegate object CreateInstanceDelegate();
        // Dictionary for holding object instantiator delegates
        private static Dictionary<Type, CreateInstanceDelegate>
            _createInstanceDelegateList = new Dictionary<Type, CreateInstanceDelegate>();


        // Function that creates the method dynamically for creating the instance
        // of a given class type
        public static CreateInstanceDelegate ObjectInstantiater(Type objectType)
        {
            CreateInstanceDelegate createInstanceDelegate;

            if (!_createInstanceDelegateList.TryGetValue(objectType,
                out createInstanceDelegate))
            {
                lock (objectType)
                {
                    if (!_createInstanceDelegateList.TryGetValue(objectType,
                 out createInstanceDelegate))
                    {
                        // Create a new method.        
                        DynamicMethod dynamicMethod =
                            new DynamicMethod("Create_" + objectType.Name,
                       objectType, new Type[0]);

                        // Get the default constructor of the plugin type
                        ConstructorInfo ctor = objectType.GetConstructor(new Type[0]);

                        // Generate the intermediate language.       
                        ILGenerator ilgen = dynamicMethod.GetILGenerator();
                        ilgen.Emit(OpCodes.Newobj, ctor);
                        ilgen.Emit(OpCodes.Ret);

                        // Create new delegate and store it in the dictionary
                        createInstanceDelegate = (CreateInstanceDelegate)dynamicMethod
                            .CreateDelegate(typeof(CreateInstanceDelegate));
                        _createInstanceDelegateList[objectType] = createInstanceDelegate;
                    }
                }
            }
            return createInstanceDelegate; // return the object instantiator delegate
        }
    }
}
