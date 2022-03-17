namespace Generics
{
    using System;

    public class MyList<T> 
    {
        private int maxSize;

        private int count;

        public MyList(int size)
        {
            this.MaxSize = size;

            OwnList = new T[size];

            count = 0;
        }

        public int MaxSize
        {
            get
            {
                return this.maxSize;
            }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(this.MaxSize), "The specified max size must be greather than 1!");
                }

                this.maxSize = value;
            }
        }

        public T[] OwnList
        {
            get;
            set;
        }

        public void Add(T item)
        {
            if (count >= MaxSize)
            {
                OwnList[MaxSize - 1] = item;
            }

            OwnList[count] = item;

            if (count >= MaxSize - 1)
            {
                count--;
            }

            count++;
        }

        public void Print()
        {           
            var enumerator = OwnList.GetEnumerator();

            while (enumerator.MoveNext() && enumerator.Current != null)
            {
                Console.WriteLine(enumerator.Current.ToString());
            }
        }

        public int Count()
        {
            int counter = 0;

            foreach (var item in OwnList)
            {
                counter++;
            }

            return counter;
        }
    }
}
