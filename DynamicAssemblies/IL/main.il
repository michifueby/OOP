.assembly extern mscorlib
{
	.publickeytoken = (b7 7a 5c 56 19 34 e0 89)
	.ver 4:0:0:0
}

.assembly CIL_Demo
{
}

.module CIL_Demo.exe

.namespace DemoNamespace
{
	.class Program extends [mscorlib]System.Object
	{
		.method static void Main(string[] args) cil managed
		{
			.entrypoint
			//.maxstack2
			ldstr "Hello World"
			call void [mscorlib]System.Console::WriteLine(string)	
			
			ret
		}

	}
	
	.class TestClass extends [mscorlib]System.Object
	{
		.method void .ctor() cil managed
		{
			ldarg.0 
			call instance void [mscorlib]System.Object::.ctor()
			ret
		}

	}
}