namespace Serialize
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Serialization;
    
    public class Program
    {
        static void Main(string[] args)
        {
            Person firstPerson = new Person() { Firstname = "Michael", Lastname = "Mustermann", Birthay = new DateTime(2000, 3, 1, 7, 0, 0) };

            Person secondPerson = new Person() { Firstname = "Max", Lastname = "Mustermann", Birthay = new DateTime(2008, 10, 8, 7, 0, 0) };

            // Serialize in .txt with binary formatter
            using (FileStream fileStream = new FileStream("Person.txt", FileMode.Create, FileAccess.ReadWrite))
            {
                IFormatter formatter = new BinaryFormatter();
                
                formatter.Serialize(fileStream, firstPerson);
                fileStream.Seek(0, SeekOrigin.Begin);

                var deserializeFirstPerson = (Person)formatter.Deserialize(fileStream);
            }

            // Serialize in .xml with xmlSerializer
            using (FileStream xmlStream = new FileStream("Person.xml", FileMode.Create, FileAccess.ReadWrite))
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(Person));

                xmlFormatter.Serialize(xmlStream, secondPerson);
                xmlStream.Seek(0, SeekOrigin.Begin);

                var deserializeSecondPerson = (Person)xmlFormatter.Deserialize(xmlStream);
            }

            // Serialize with Surroagte
            using (FileStream fileStream = new FileStream("Person2.txt", FileMode.Create, FileAccess.ReadWrite))
            {
                SurrogateSelector surrogateSelector = new SurrogateSelector();
                surrogateSelector.AddSurrogate(typeof(Person), new StreamingContext(StreamingContextStates.All), new PersonSurrogate());

                IFormatter binaryFormatter = new BinaryFormatter();

                binaryFormatter.SurrogateSelector = surrogateSelector;
                
                // Serialize 
                binaryFormatter.Serialize(fileStream, secondPerson);
                
                // Deserialize
                fileStream.Seek(0, SeekOrigin.Begin);
                Person person2 = (Person)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
        }
    }
}
