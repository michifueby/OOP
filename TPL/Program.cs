namespace TPL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        public delegate int BinaryOperation(int firstNumber, int secondNumber);

        public static void Main(string[] args)
        {
            // Asynchronous Delegates
            BinaryOperation binaryOperation = new BinaryOperation(Add);

            var asyncResult = binaryOperation.BeginInvoke(100, 100, new AsyncCallback(AddCompleteCB), "Binary operation was sucessfull!");

            while (!asyncResult.IsCompleted)
            {
                Thread.Sleep(1000);
            }

            // ThreadPool
            for (int i = 0; i < 20; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(PrintTest));
            }

            // ParallelFor
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            });

            // ParallelForEach
            Parallel.ForEach(new List<string> { "Test1", "Test2", "Test3", "Test4", "Test5" }, i =>
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            });

            // ParallelInvoke
            Action firstAction = () => { Console.WriteLine("firstAction invoked!"); };
            Action secondAction = () => { Console.WriteLine("secondAction invoked!"); };

            Parallel.Invoke(firstAction, secondAction);

            // Task
            Task myTask = Task.Factory.StartNew(() =>
             {
                 Thread.Sleep(1000);
                 Console.WriteLine($"Task {DateTime.Now}");
             });

            Console.WriteLine($"Do something in Main! {DateTime.Now}");

            myTask.Wait();

            // Async Method
            var task = ProcessAsync();
            task.Wait();

            Console.ReadKey();
        }

        private static int Add(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }

        private static void AddCompleteCB(IAsyncResult asyncResult)
        {
            AsyncResult ar = (AsyncResult)asyncResult;
            BinaryOperation binaryOperation = (BinaryOperation)ar.AsyncDelegate;

            int result = binaryOperation.EndInvoke(asyncResult);
            string message = (string)ar.AsyncState;

            Console.WriteLine($"Result is: {result}, {message}");
        }

        private static void PrintTest(object data)
        {
            Console.WriteLine($"This is a test, threadID: {Thread.CurrentThread.ManagedThreadId}");
        }

        // Async Method
        private static async Task ProcessAsync()
        {
            Console.WriteLine("Starting...");

            Task[] tasks = new Task[5];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = CalculateAsync(10 * 1000000 / 2);
            }

            int result = HeavyCalculate(10 * 70000);

            Console.WriteLine("Process heavy async heavy work done!");

            foreach (var task in tasks)
            {
                await task;
            }

            Console.WriteLine("ProcessAsync() await done!");
        }

        private static async Task CalculateAsync(int value)
        {
            Console.WriteLine($"Calculate value: {value}, {Thread.CurrentThread.ManagedThreadId}");

            await Task.Run<int>(() => { return HeavyCalculate(value); });

            Console.WriteLine($"Calculation waiting for done, {Thread.CurrentThread.ManagedThreadId}");
        }

        private static int HeavyCalculate(int value)
        {
            Console.WriteLine("Start heavy calculation!");

            int result = (value * 5000) % 2;

            for (int i = 0; i < 10000; i++)
            {
                result++;
            }

            Console.WriteLine($"Calculate result from value, {Thread.CurrentThread.ManagedThreadId}!");

            return result;
        }
    }
}
