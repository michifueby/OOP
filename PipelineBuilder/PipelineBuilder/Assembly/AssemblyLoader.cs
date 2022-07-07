namespace PipelineBuilder
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class AssemblyLoader
    {
        /// <summary>
        /// Declare the logger.
        /// </summary>
        private Logger logger;

        /// <summary>
        /// Declare the loaded assemblies.
        /// </summary>
        private List<Assembly> loadedAssemblies;

        /// <summary>
        /// Initialize a new instance of the <see cref="AssemblyLoader"./>
        /// </summary>
        public AssemblyLoader()
        {
            this.loadedAssemblies = new List<Assembly>();

            this.logger = new Logger();
        }

        /// <summary>
        /// Gets the loaded assemblies.
        /// </summary>
        /// <value>The loaded assemblies.</value>
        public List<Assembly> LoadedAssemblies
        {
            get
            {
                return this.loadedAssemblies;
            }
        }

        /// <summary>
        /// Load assemblies from path.
        /// </summary>
        /// <param name="path">Specified the assembly path.</param>
        public void LoadAssemblyFromPath(string path)
        {
            Assembly assembly;

            try
            {
                foreach (var file in Directory.EnumerateFiles(path))
                {
                    assembly = Assembly.LoadFrom(file);
                    this.loadedAssemblies.Add(assembly);

                    logger.LogToConsole(ConsoleColor.Green, $"The assembly \"{Path.GetFileName(file)}\" was sucessfully loaded!");
                }
              
            }
            catch (Exception)
            {
                logger.LogToConsole(ConsoleColor.Red, "Assembly cannot be loaded!");
            }
        }
    }
}