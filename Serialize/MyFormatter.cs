namespace Serialize
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public class MyFormatter : IFormatter 
    {
        public MyFormatter()
        {
            this.Context = new StreamingContext(StreamingContextStates.All);
        }

        public ISurrogateSelector SurrogateSelector
        {
            get;
            set;
        }

        public SerializationBinder Binder
        {
            get;
            set;
        }

        public StreamingContext Context
        {
            get;
            set;
        }

        public object Deserialize(Stream serializationStream)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            
            return binaryFormatter.Deserialize(serializationStream);
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(serializationStream, graph);
        }
    }
}
