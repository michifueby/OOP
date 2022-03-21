namespace Serialize
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class Person
    {
        [NonSerialized]
        private string secret;

        public string Firstname
        {

            get;
            set;
        }

        public string Lastname
        {
            get;
            set;
        }

        public DateTime Birthay
        {
            get;
            set;
        }

        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            Console.WriteLine("This is getting serialized now!");
        }

        [OnSerialized]
        public void OnSerialized(StreamingContext context)
        {
            Console.WriteLine("This was serialized now!");
        }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext context)
        {
            Console.WriteLine("This is getting deserialized now!");
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            Console.WriteLine("This was deserialized now!");
        }
    }
}
