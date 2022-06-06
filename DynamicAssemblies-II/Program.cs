namespace DynamicAssemblies_II
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Create AppDomain
            AppDomain app = AppDomain.CurrentDomain;

            // Define Name and Version of Assembly
            AssemblyName assemblyName = new AssemblyName("MyAssembly")
            {
                Version = new Version("1.0.0.0")
            };

            // Define Dynamic Assembly 
            AssemblyBuilder assemblyBuilder = app.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);

            // Define Dynamic Module
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MyDynamicAssembly", "MyDynamicAssembly.dll");

            // Define a new Type
            TypeBuilder typeBuilder = moduleBuilder.DefineType("MyClass");

            // Define a method
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("AddNumbers", MethodAttributes.Public, typeof(int), new Type[] { typeof(int), typeof(int) });

            ILGenerator iLGenerator = methodBuilder.GetILGenerator();
            // Load argument at index 1 and 2
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Ldarg_2);

            // Add the Add method
            iLGenerator.Emit(OpCodes.Add);
            iLGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }));

            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Ldarg_2);

            iLGenerator.Emit(OpCodes.Add);
            iLGenerator.Emit(OpCodes.Ret);

            //Store assembly
            typeBuilder.CreateType();
            assemblyBuilder.Save("MyDynamicAssembly.dll");

            // Load Assembly and invoke method
            Assembly assembly = Assembly.LoadFrom("MyDynamicAssembly.dll");
            Type type = assembly.GetType("MyClass");
            MethodInfo info = type.GetMethod("AddNumbers");
            var myDynamicAssemblyObj = Activator.CreateInstance(type, new object[0]);
            Console.WriteLine("The result is " + info.Invoke(myDynamicAssemblyObj, new object[] { 10, 2 }));

            Console.ReadKey();
        }
    }
}
