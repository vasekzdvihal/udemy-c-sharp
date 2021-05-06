using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Channels;

namespace Playground.Linq
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public Computer Computer { get; set; }

        public Person()
        {
            
        }
        
        public Person(int id, string name, Position position, Computer computer)
        {
            Id = id;
            Name = name;
            Position = position;
            Computer = computer;
        }

        public List<Person> PopulatePersonsFirstList()
        {
            var result = new List<Person>();

            result.Add(new Person(1, "John", Position.Ceo, new Computer(1, "DELL", Type.Desktop, "CEO's Desktop")));
            result.Add(new Person(2, "Jane", Position.Developer, new Computer(2, "DELL", Type.Laptop, "Jane's Laptop")));
            result.Add(new Person(3, "Shuba", Position.Manager, new Computer(3, "Lenovo", Type.Laptop, "Shuba's Laptop")));
            
            return result;
        }

        
        public List<Person> PopulatePersonsSecondList()
        {
            var result = new List<Person>();

            result.Add(new Person(4, "Julie", Position.Coo, new Computer(6, "HP", Type.Desktop, "COO's Desktop")));
            result.Add(new Person(5, "Alan", Position.Developer, new Computer(4, "DELL", Type.Desktop, "Alan's Laptop")));
            result.Add(new Person(6, "Ava", Position.Manager, new Computer(2, "DELL", Type.Desktop, "Ava's Laptop")));
            
            return result;
        }
        
        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Position)}: {Position}, {nameof(Computer)}: {Computer}";
        }
    }

    public class Computer
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public Type Type { get; set; }
        public string Description { get; set; }

        public Computer(int id, string manufacturer, Type type, string description)
        {
            Id = id;
            Manufacturer = manufacturer;
            Type = type;
            Description = description;
        }
        
        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Manufacturer)}: {Manufacturer}, {nameof(Type)}: {Type}, {nameof(Description)}: {Description}";
        }
    }

    public enum Type { Desktop, Laptop }
    public enum Position { Developer, Manager, Ceo, Coo }
    
    class Program
    {
        static void Main(string[] args)
        {
            var first = new Person().PopulatePersonsFirstList();
            var second = new Person().PopulatePersonsSecondList();
            
            Console.WriteLine("Populated Lists");
            ShowResult(first, "First");
            ShowResult(second, "Second");
            
            // Union
            var union = first.Union(second);
            ShowResult(union, "Union");
            
            // Select just Laptops
            var laptops = union.Where(x => x.Computer.Type == Type.Laptop).ToList();
            ShowResult(laptops, "Select just Laptops (Where)");
            
            // Select just names
            var names = union.Select(x => x.Name);
            ShowResult(names, "Just names");
            
            // Select just Computer object
            var computers = union.Select(x => x.Computer);
            ShowResult(computers, "Select just computer objects");
            
            // Group by positons
            var groupByPosition = union.GroupBy(x => x.Position).ToList();
            foreach(var group in groupByPosition) 
                ShowResult(group, $"Group with position {group.FirstOrDefault()?.Position}");
        }

        private static void ShowResult<T>(IEnumerable<T> list, string action = "") 
        {
            Console.WriteLine(action);
            foreach (var l in list)
                Console.WriteLine(l);
        }
    }
}