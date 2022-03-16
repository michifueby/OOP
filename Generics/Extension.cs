namespace Generics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Extension
    {
        // .Print()
        public static void MyPrint<T>(this IEnumerable<T> items)
        {
            var enumerator = items.GetEnumerator();

            //enumerator.Reset();

            while (enumerator.MoveNext())
            {
                Console.Write(enumerator.Current.ToString());

                Console.Write(", ");
            }
        }

        // .MyNormalPrint()
        public static void MyNormalPrint<T>(this IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        // .Append()
        public static IEnumerable<T> MyAppend<T>(this T item, IEnumerable<T> items)
        {
            foreach (var i in items)
            {
                yield return i;
            }
        }

        // .MyAny
        public static bool MyAny<T>(this IEnumerable<T> items)
        {
            var enumerator = items.GetEnumerator();

            enumerator.Reset();

            if (!enumerator.MoveNext())
                return enumerator.MoveNext();

            return true;
        }

        // .MySkip
        public static IEnumerable<T> MySkip<T>(this IEnumerable<T> items, int count)
        {
            int i = 0;

            foreach (var item in items)
            {
                items.GetEnumerator().MoveNext();

                if (i >= count)
                {
                    yield return item;
                }

                i++;
            }
        }

        // .MyTake()
        public static IEnumerable<T> MyTake<T>(this IEnumerable<T> items, int counter)
        {
            int i = 0;

            foreach (var item in items)
            {
                items.GetEnumerator().MoveNext();

                if (i < counter)
                {
                    yield return item;
                    i++;
                }
            }
        }

        // .MyFirstElement() 
        public static T MyFirstElement<T>(this IEnumerable<T> items)
        {
            var enumerator = items.GetEnumerator();

            enumerator.MoveNext();
            return enumerator.Current;
        }

        // .MyCount()
        public static int MyCount<T>(IEnumerable<T> items)
        {
            int counter = 0;

            foreach (var item in items)
            {
                counter++;
            }

            return counter;
        }
    }
}
