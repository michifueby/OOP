namespace PipelineBuilder
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Represents the application class.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Declare the assembly loader.
        /// </summary>
        private AssemblyLoader assemblyLoader;

        /// <summary>
        /// Declare the assembly manager.
        /// </summary>
        private AssemblyManager assemblyManager;

        /// <summary>
        /// Declare the method connector.
        /// </summary>
        private MethodConnector methodConnector;

        /// <summary>
        /// Declare the logger.
        /// </summary>
        private Logger logger;

        /// <summary>
        /// Declare the application state.
        /// </summary>
        private bool isExit;

        /// <summary>
        /// Run the application.
        /// </summary>
        public void Run()
        {
            this.logger = new Logger();

            this.isExit = false;

            this.ShowInfo();

            this.assemblyLoader = new AssemblyLoader();
            this.assemblyLoader.LoadAssemblyFromPath(Directory.GetCurrentDirectory() + @"\plugins\");

            this.assemblyManager = new AssemblyManager(assemblyLoader.LoadedAssemblies);
            this.methodConnector = new MethodConnector(assemblyManager.Methods);

            this.ReadConsoleInput();
        }

        /// <summary>
        /// Show info text.
        /// </summary>
        private void ShowInfo()
        {
            logger.LogToConsole(Console.ForegroundColor, "PipelineBuilder\n\n" + 
                                                        "[show methods]       Show all methods from Assembly\n" +
                                                        "[0>1]                Connect a method with another method, use the index of the method\n" +
                                                        "[execute]            Execute the pipeline\n" +
                                                        "[exit]               Close the PipelineBuilder\n");
        }

        /// <summary>
        /// Read the console input.
        /// </summary>
        private void ReadConsoleInput()
        {
            while (!isExit)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(">");
                string input = Console.ReadLine();

                this.ProcessInput(input);
            }
        }

        /// <summary>
        /// Process the input.
        /// </summary>
        /// <param name="input">Specified the input.</param>
        private void ProcessInput(string input)
        {
            if (input.Contains('>'))
            {
                this.methodConnector.Connect(this.assemblyManager.TypesOfAssemblies, input);
            }
            else
            {
                switch (input.ToLower())
                {
                    case "exit":
                        this.isExit = true;
                        break;

                    case "show methods":
                        this.ShowAllMethodsFromAssembly();
                        break;

                    case "execute":
                        this.methodConnector.Execute();
                        break;

                    default:
                        logger.LogToConsole(ConsoleColor.Red, "Invalid command!");
                        break;
                }
            }
        }

        /// <summary>
        /// Show all methods from assemblies.
        /// </summary>
        private void ShowAllMethodsFromAssembly()
        {
            var methods = this.assemblyManager.GetMethodsFromAssembly();

            Console.WriteLine("\nMethods:");

            foreach (var method in methods)
            {
                var methodIndex = $"{methods.IndexOf(method)}" + " ";
                
                Console.Write(methodIndex);
                Console.Write(method.ReturnType);
                Console.Write(" ");
                Console.Write(method.Name);

                foreach (var parameter in method.GetParameters())
                {
                    var parameterInfo = $"({parameter.ParameterType})" + " ";
                    Console.Write(parameterInfo);
                }

                foreach (var methodDescription in method.GetCustomAttributes().Where(a => a.GetType().Equals(typeof(PipelineDescriptionAttribute))))
                {
                    
                    var methodDescriptionInfo = $": {((PipelineDescriptionAttribute)methodDescription).Description}";
                    Console.Write(methodDescriptionInfo);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}