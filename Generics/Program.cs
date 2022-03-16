namespace Generics
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int[] intArray = { 1, 2, 3, 4, 5 };

            int[] intArray2 = { 6, 7, 8, 9, 10 };

            // .Print()
            intArray.MyPrint();

            // .Append()
            intArray[4].MyAppend(intArray2).MyPrint();

            // .MyAny()
            bool isNotEmpty = intArray.MyAny();

            // .MySkip()
            intArray.MySkip(3).MyNormalPrint();

            // .MyTake()
            intArray.MyTake(2).MyNormalPrint();

            // .MyFirstElement
            int firstNumber = intArray2.MyFirstElement();

            Console.ReadKey();

        }
    }
}