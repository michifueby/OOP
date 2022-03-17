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
            Console.WriteLine(firstNumber);

            // Generic Class with data structure
            MyList<object> myList = new MyList<object>(7);
            string firstContent = "Test";
            string secondContent = "Test2";

            // Add elements to array
            myList.Add(firstContent);
            myList.Add(firstContent);
            myList.Add(firstContent);
            myList.Add(secondContent);

            // Print elements
            myList.Print();
            
            // Print count of elements
            int count = myList.Count();
            Console.WriteLine(count);

            Console.ReadKey();

        }
    }
}