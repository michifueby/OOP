namespace DynamicAssemblies
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Choose CurrentDomain (Default Domain)
            AppDomain app = AppDomain.CurrentDomain;

            // Create new Assembly with version
            AssemblyName assemblyName = new AssemblyName("MyDynamicAssembly") 
            { 
                Version = new Version("1.0.0.0") 
            };

            AssemblyBuilder assemblyBuilder = app.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);

            ModuleBuilder modBuilder = assemblyBuilder.DefineDynamicModule("MyDynamicAssembly", "MyDynamicAssembly.dll");

            // Define TestClass 
            TypeBuilder classBuilder = modBuilder.DefineType("MyDynamicAssembly.TestClass", TypeAttributes.Public);

            // Define field with typeof string
            FieldBuilder fieldBuilder = classBuilder.DefineField("message", typeof(string), FieldAttributes.Public);

            // Create constructor
            ConstructorBuilder constructorBuilder = classBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(string) });

            ILGenerator ilGenerator = constructorBuilder.GetILGenerator();

            // Load argument at index 0 -> this
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[0]));
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            ilGenerator.Emit(OpCodes.Ret);

            // Create methode
            MethodBuilder methBuilder = classBuilder.DefineMethod("Print", MethodAttributes.Public);
            ilGenerator = methBuilder.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            ilGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            ilGenerator.Emit(OpCodes.Ret);

            // Create the whole type
            classBuilder.CreateType();

            // Save the assembly 
            assemblyBuilder.Save("MyDynamicAssembly.dll");

            var loadedAssembly = Assembly.LoadFile(@"C:\Users\michaelfuby\Documents\Development\C#\OOP\DynamicAssemblies\bin\Debug\MyDynamicAssembly.dll");
            var assemblyType = loadedAssembly.GetType("MyDynamicAssembly.TestClass");

            // Print methods from assembly
            Console.WriteLine("Methods:");
            foreach (var method in assemblyType.GetMethods())
            {
                Console.WriteLine(method.Name);
            }

            // Print fields from assembly
            Console.WriteLine("Fields:");
            foreach (var field in assemblyType.GetFields())
            {
                Console.WriteLine(field.Name);
            }

            Console.ReadKey();
        }
    }
}
