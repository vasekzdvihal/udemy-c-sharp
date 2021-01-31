using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Prototype.Serialization
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();

            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);
            }
        }
    }

    [Serializable]
    public class Address
    {
        public string StreetAddress, City, Country;

        public Address()
        {
            
        }

        public Address(string streetAddress, string city, string country)
        {
            StreetAddress = streetAddress;
            City = city;
            Country = country;
        }

        public Address(Address other)
        {
            StreetAddress = other.StreetAddress;
            City = other.City;
            Country = other.Country;
        }

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Country)}: {Country}";
        }
    }

    [Serializable]
    public class Employe
    {
        public string Name;
        public Address Address;

        public Employe()
        {
            
        }

        public Employe(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public Employe(Employe other)
        {
            Name = other.Name;
            Address = new Address(other.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Employe("John", new Address("123 Long Road", "London", "UK"));

            var lisa = john.DeepCopy();
            lisa.Name = "Lisa";
            lisa.Address.City = "Brno";
            lisa.Address.Country = "Asia";

            var jane = john.DeepCopyXml();
            jane.Name = "Jane";
            jane.Address.City = "New York";
            jane.Address.Country = "USA";

            Console.WriteLine(john);
            Console.WriteLine(lisa);
            Console.WriteLine(jane);
        }

    }
}