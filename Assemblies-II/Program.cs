namespace Assemblies_II
{
    using System;
    using System.Reflection;

    public class Program
    {
        static void Main(string[] args)
        {
            // Analyse a program
            var loadedAssembly = Assembly.LoadFile(@"C:\Temp\ExitTheRoom.exe");

            var programmClass = loadedAssembly.GetType("ExitTheRoom.Program");
            var secondClass = loadedAssembly.GetType("ExitTheRoom.DaenerysMotherOfPrivateMethods");

            object obj = Activator.CreateInstance(secondClass);

            var methods = secondClass.GetMethods(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            
            foreach (var method in methods)
            {
                Console.WriteLine("Method name: " + method.Name + "  " + "Return type:" + method.ReturnType);
            }

            Console.ReadKey();
        }
    }
}
