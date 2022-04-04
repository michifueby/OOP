namespace Serialize
{
    using System.Runtime.Serialization;

    public class PersonSurrogate : ISerializationSurrogate 
    {
        public void GetObjectData(object obj, SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Person person = (Person)obj;

            serializationInfo.AddValue("Firstname", person.Firstname);
            serializationInfo.AddValue("Lastname", person.Lastname);
            serializationInfo.AddValue("Birthday", person.Birthay);
        }

        public object SetObjectData(object obj, SerializationInfo serializationInfo, StreamingContext streamingContext, ISurrogateSelector surrogateSelector)
        {
            Person person = (Person)obj;

            person.Firstname = serializationInfo.GetString("Firstname");
            person.Lastname = serializationInfo.GetString("Lastname");
            person.Birthay = serializationInfo.GetDateTime("Birthday");

            return person;
        }
    }
}
