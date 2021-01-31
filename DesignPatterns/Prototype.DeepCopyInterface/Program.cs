using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.DeepCopyInterface
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }


    public class Address : IPrototype<Address>
    {
        public string StreetAddress, City, Country;

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

        public Address DeepCopy()
        {
            return new Address(StreetAddress, City, Country);
        }

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Country)}: {Country}";
        }
    }

    public class Employe : IPrototype<Employe>
    {
        public string Name;
        public Address Address;

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

        public Employe DeepCopy()
        {
            return new Employe(Name, Address);
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
            Console.WriteLine(lisa);
        }

    }
}