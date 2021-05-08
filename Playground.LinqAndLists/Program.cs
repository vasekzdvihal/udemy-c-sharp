using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Channels;

namespace Playground.Linq
{
    public class Person : IEquatable<Person>
    {
        public int Id { get; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public Position Position { get; set; }
        public Computer Computer { get; set; }

        public Person()
        {
            
        }

        public Person(int id, string name, double salary, Position position, Computer computer)
        {
            Id = id;
            Name = name;
            Salary = salary;
            Position = position;
            Computer = computer;
        }

        public List<Person> PopulatePersonsFirstList()
        {
            var result = new List<Person>();

            result.Add(new Person(1, "John", 10000.00, Position.Ceo, new Computer(1, "DELL", Type.Desktop, "CEO's Desktop")));
            result.Add(new Person(2, "Jane", 15000.50, Position.Developer, new Computer(2, "DELL", Type.Laptop, "Jane's Laptop")));
            result.Add(new Person(3, "Shuba",15525.99, Position.Manager, new Computer(3, "Lenovo", Type.Laptop, "Shuba's Laptop")));
            result.Add(new Person(3, "Shuba",99955.00, Position.Manager, new Computer(3, "Lenovo", Type.Laptop, "Shuba's Laptop")));
            
            return result;
        }

        
        public List<Person> PopulatePersonsSecondList()
        {
            var result = new List<Person>();

            result.Add(new Person(4, "Julie", 15525.99, Position.Coo, new Computer(6, "HP", Type.Desktop, "COO's Desktop")));
            result.Add(new Person(4, "Skylar", 134525.99, Position.Coo, new Computer(12, "", Type.Desktop, "COO's Desktop")));
            result.Add(new Person(5, "Alan", 14325525.99, Position.Developer, new Computer(4, "DELL", Type.Desktop, "Alan's Laptop")));
            result.Add(new Person(6, "Ava", 1554425.99, Position.Manager, new Computer(2, "DELL", Type.Desktop, "Ava's Laptop")));
            
            return result;
        }

        public bool Equals(Person other)
        {
            return this.Id == other?.Id;
        }

        public override int GetHashCode()
        {
            var hashId = Id.GetHashCode();
            return hashId;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Salary)}: {Salary}, {nameof(Position)}: {Position}, {nameof(Computer)}: {Computer}";
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
            
            var all = new List<Person>();
            all.AddRange(first);
            all.AddRange(second);
            
            Console.WriteLine("Populated Lists");
            ShowResult(first, "First");
            ShowResult(second, "Second");
            ShowResult(all, "ALL");
            
            // Union
            var union = first.Union(second);
            ShowResult(union, "Union");
            
            // Select just Laptops
            var laptops = all.Where(x => x.Computer.Type == Type.Laptop).ToList();
            ShowResult(laptops, "Select just Laptops (Where)");
            
            // Select just names
            var names = all.Select(x => x.Name);
            ShowResult(names, "Just names");
            
            // Select just Computer object
            var computers = all.Select(x => x.Computer);
            ShowResult(computers, "Select just computer objects");
            
            // Group by positons
            var groupByPosition = all.GroupBy(x => x.Position).ToList();
            ShowResultGroups(groupByPosition);
                
            // Distinct 
            var distinctPersons = all.Distinct();
            ShowResult(distinctPersons, "Distinct Persons");
            
            // Average 
            var averageSalary = all.Average(x => x.Salary);
            ShowResult(averageSalary, "Average Salary");
        }

        private static void ShowResultGroups(List<IGrouping<Position, Person>> groups, string groupName = "")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(groupName);
            Console.ForegroundColor = ConsoleColor.White;
            
            foreach(var group in groups)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(group.FirstOrDefault()?.Position);
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var l in group)
                {
                    Console.WriteLine(l);
                }
            }

            Console.WriteLine();
            Console.WriteLine();
        }
        
        private static void ShowResult<T>(IEnumerable<T> list, string action = "")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(action);
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var l in list)
                Console.WriteLine(l);
            
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void ShowResult<T>(T result, string action = "")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(action);
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine(result);
            
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}