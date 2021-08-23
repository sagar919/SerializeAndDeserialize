using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationBinary
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person() { FirstName = "Sagar", LastName = "lad" };
            string filePath = "data.save";

            DataSerializer dataSerializer = new DataSerializer();
            Person p = null;

            dataSerializer.BinarySerialize(person, filePath);
            p = dataSerializer.BinaryDeserialize(filePath) as Person;

            Console.WriteLine(p.FirstName);
            Console.WriteLine(p.LastName);

        }

        [Serializable]
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        class DataSerializer
        {
            public void BinarySerialize(object data, string filePath)
            {
                FileStream fileStream;
                BinaryFormatter bf = new BinaryFormatter();

                if (File.Exists(filePath))
                    File.Delete(filePath);

                fileStream = File.Create(filePath);
                bf.Serialize(fileStream, data);

                fileStream.Close();
            }


            public object BinaryDeserialize(string filePath)
            {
                object obj = null;

                FileStream fileStream;
                BinaryFormatter bf = new BinaryFormatter();
                if (File.Exists(filePath))
                {
                    fileStream = File.OpenRead(filePath);
                    obj = bf.Deserialize(fileStream);
                    fileStream.Close();
                }

                return obj;
            }

        }
    }
}
