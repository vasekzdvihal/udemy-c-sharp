using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Excercise
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }

    public class PersonFactory
    {
        private int id = 0;

        public Person CreatePerson(string name)
        {
            return new Person { Id = id++, Name = name };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var factory = new PersonFactory();
            var jack = factory.CreatePerson("Jack");
            var sarah = factory.CreatePerson("Sarah");
            Console.WriteLine(jack);
            Console.WriteLine(sarah);
        }
    }
}
