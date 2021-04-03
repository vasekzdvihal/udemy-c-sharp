using System;
using System.Reflection.Metadata;

namespace Proxy.Exercise
{
    public class Person
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }
    public class ResponsiblePerson
    {
        private readonly Person person;
        
        public ResponsiblePerson(Person person)
        {
            this.person = person;
        }

        public int Age
        {
            get { return person.Age; }
            set { person.Age = value; }
        }


        public string Drive()
        {
            if (Age >= 16)
                return person.Drive();
            return "too young";
        }

        public string Drink()
        {
            if (Age >= 18)
                return person.Drink();
            return "too young";
        }

        public string DrinkAndDrive()
        {
            return "dead";
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}