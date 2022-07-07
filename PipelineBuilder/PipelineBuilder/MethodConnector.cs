namespace PipelineBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Represents the method connector.
    /// </summary>
    public class MethodConnector
    {
        /// <summary>
        /// Declare the logger.
        /// </summary>
        private Logger logger;

        /// <summary>
        /// Declare the methods.
        /// </summary>
        private List<MethodInfo> methods;

        /// <summary>
        /// Declare the connections.
        /// </summary>
        private List<MethodConnection> connections;

        /// <summary>
        /// Declare the state about the last method.
        /// </summary>
        private bool isLastMethodFound;

        public MethodConnector(List<MethodInfo> methods)
        {
            logger = new Logger();

            this.methods = methods;
            this.connections = new List<MethodConnection>();
        }

        /// <summary>
        /// Connect the methods.
        /// </summary>
        /// <param name="loadedTypesFromAssemblies">Specified the loaded types of assemblies.</param>
        /// <param name="input">Specified the input.</param>
        public void Connect(List<Type> loadedTypesFromAssemblies, string input)
        {
            int firstIndex;
            int secondIndex;
            bool isIndexValid;

            string[] numbers = input.Split('>');

            isIndexValid = int.TryParse(numbers[0], out firstIndex);
            isIndexValid = int.TryParse(numbers[1], out secondIndex);

            if (!isIndexValid)
            {
                logger.LogToConsole(ConsoleColor.Red, "Invalid method index!");
                return;
            }

            if (!CheckMethodCompability(firstIndex, secondIndex))
            {
                logger.LogToConsole(ConsoleColor.Red, "Methods cannot be connected! The reason is Incompability!");
                return;
            }

            foreach (var connection in this.connections)
            {
                if (connection.SenderMethod == this.methods[firstIndex] && connection.ReceiverMethod == this.methods[secondIndex])
                {
                    logger.LogToConsole(ConsoleColor.Red, "The connection already exists!");
                    return;
                }
            }

            connections.Add(new MethodConnection(methods[firstIndex], methods[secondIndex]));

            logger.LogToConsole(ConsoleColor.Green, "Connection was successfull!");
        }

        /// <summary>
        /// Check the method compability.
        /// </summary>
        /// <param name="firstIndex">Specified the first index.</param>
        /// <param name="secondIndex">Specified the second index.</param>
        /// <returns>The value of the compability.</returns>
        private bool CheckMethodCompability(int firstIndex, int secondIndex)
        {
            if (this.methods[secondIndex].GetParameters().Length > 1 || this.methods[secondIndex].GetParameters().Length == 0)
            {
                return false;
            }
            
            return this.methods[firstIndex].ReturnType == this.methods[secondIndex].GetParameters()[0].ParameterType;
        }

        /// <summary>
        /// Execute the pipeline.
        /// </summary>
        public void Execute()
        {
            MethodInfo entryPoint = null;

            foreach (var connection in this.connections)
            {
                if (connection.SenderMethod.GetParameters().Count() == 0)
                {
                    entryPoint = connection.SenderMethod;
                    break;
                }
            }

            if (entryPoint == null)
            {
                logger.LogToConsole(ConsoleColor.Red, "Something went wrong while executing!");
                return;
            }

            object obj = new object();

            object typeObject = Activator.CreateInstance(entryPoint.DeclaringType);
            obj = entryPoint.Invoke(typeObject, null);

            var previousMethod = entryPoint;

            do
            {
                var nextMethod = this.FindReceiverForSender(previousMethod);

                if (this.isLastMethodFound)
                {
                    logger.LogToConsole(ConsoleColor.Green, "Execution finished successfully!");
                    return;
                }

                obj = this.InvokeMethod(nextMethod, obj);
                previousMethod = nextMethod;
            } 
            while (true);
        }

        /// <summary>
        /// Invoke a method.
        /// </summary>
        /// <param name="method">Specified the method.</param>
        /// <param name="parameter">Specified the parameter.</param>
        /// <returns>The the return value of the method.</returns>
        private object InvokeMethod(MethodInfo method, object parameter)
        {
            object temp = new object();

            if (method.IsStatic)
            {
                temp = method.Invoke(null, new object[] { parameter });
            }
            else
            {
                object typeObject = Activator.CreateInstance(method.DeclaringType);
                temp = method.Invoke(typeObject, new object[] { parameter });
            }

            return temp;
        }

        /// <summary>
        /// Find the receiver for the sender.
        /// </summary>
        /// <param name="senderMethod">Specified the sender method.</param>
        /// <returns>The receiver method.</returns>
        private MethodInfo FindReceiverForSender(MethodInfo senderMethod)
        {
            foreach (var connection in this.connections)
            {
                if (connection.SenderMethod == senderMethod)
                {
                    return connection.ReceiverMethod;
                }
            }

            this.isLastMethodFound = true;

            return null;
        }
    }
}