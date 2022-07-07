namespace PipelineBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents the AssemblyManager class.
    /// </summary>
    public class AssemblyManager
    {
        /// <summary>
        /// Declare the loaded assemblies.
        /// </summary>
        private List<Assembly> loadedAssemblies;

        /// <summary>
        /// Declare the types of assemblies.
        /// </summary>
        private List<Type> typesOfAssemblies;

        /// <summary>
        /// Declare the methods of assemblies.
        /// </summary>
        private List<MethodInfo> methods;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyManager"/>.
        /// </summary>
        /// <param name="loadedAssemblies">Specified the loaded assemblies.</param>
        public AssemblyManager(List<Assembly> loadedAssemblies)
        {
            this.typesOfAssemblies = new List<Type>();
            this.loadedAssemblies = loadedAssemblies;
            this.methods = new List<MethodInfo>();

            this.GetTypeFromAssemblies();
        }

        /// <summary>
        /// Get the types of the assemblies.
        /// </summary>
        /// <value>The type of the assembly.</value>
        public List<Type> TypesOfAssemblies
        {
            get
            {
                return this.typesOfAssemblies;
            }
        }

        /// <summary>
        /// Gets the methods.
        /// </summary>
        /// <value>The methods of assemblies.</value>
        public List<MethodInfo> Methods
        {
            get
            {
                return this.methods;
            }
        }

        /// <summary>
        /// Get methods from assembly.
        /// </summary>
        /// <returns>The method info.</returns>
        public List<MethodInfo> GetMethodsFromAssembly()
        {
            foreach (var typeOfAssembly in typesOfAssemblies)
            {               
                var methodsInfo = typeOfAssembly.GetMethods();

                foreach (var methodInfo in methodsInfo)
                {
                    if (!methodInfo.IsPublic)
                    {
                        continue;
                    }

                    var attributes = methodInfo.CustomAttributes;

                    foreach (var attribut in attributes)
                    {
                        if (attribut.AttributeType == typeof(PipelineAttribute))
                        {
                            methods.Add(methodInfo);                   
                        }
                    }
                }
            }

            return methods;
        }

        /// <summary>
        /// Get type from loaded assemlbies.
        /// </summary>
        private void GetTypeFromAssemblies()
        {
            foreach (var assembly in loadedAssemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!type.IsPublic)
                    {
                        continue;
                    }

                    typesOfAssemblies.Add(type);
                }
            }
        }
    }
}