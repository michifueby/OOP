namespace Assemblies
{
    using System;
    using System.Reflection;
    using MyLib;

    public class Program
    {
        public static void Main(string[] args)
        {
            Type type = typeof(MyClass);

            // Print all methods from MyLib
            foreach (var method in type.GetMethods())
            {
                Console.WriteLine("Method:" + method.Name);
                Console.WriteLine("Return type:" + method.ReturnType);
            }

            // Print all properties from MyLib
            foreach (var property in type.GetProperties())
            {
                Console.WriteLine("Property:" + property.Name);
                Console.WriteLine("Declaring type:" + property.DeclaringType);
                Console.WriteLine("Can read (true/false):" + property.CanRead);
            }

            // Instantiate 
            object newObject = Activator.CreateInstance(type);
            Console.WriteLine(newObject.ToString());

            // Load extern Assembly
            var loadedAssembly = Assembly.LoadFrom(@"C:\Users\michaelfuby\Documents\Development\C#\OOP\MyLib\bin\Debug\MYLib.dll");

            // Get type from assembly
            var assemblyType = loadedAssembly.GetType("MyLib.MyClass");
            
            // Get custom attributes
            var customAttributes = assemblyType.GetCustomAttributes();

            foreach (var attribut in customAttributes)
            {
                Console.WriteLine("Custom attribute:" + attribut.ToString());
            }

            Console.ReadKey();
        }
    }
}
