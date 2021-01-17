using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Builder.RecursiveGenerics
{
    public class Person
    {
        public string Name;
        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {

        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }


    public class PersonInfoBuilder<TSelf> : PersonBuilder where TSelf : PersonInfoBuilder<TSelf>
    {
        protected Person person = new Person();

        public TSelf Called(string name)
        {
            person.Name = name;
            return (TSelf)this;
        }
    }

    public class PersonJobBuilder<TSelf> : PersonInfoBuilder<PersonJobBuilder<TSelf>> where TSelf : PersonJobBuilder<TSelf>
    {
        public TSelf WorkAsA(string position)
        {
            person.Position = position;
            return (TSelf)this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var me = Person.New.Called("John").WorkAsA("Software Developer").Build();
            Console.WriteLine(me);
        }
    }
}
