using System;

namespace Decorator.MultupleInheritenceDefaultMembers
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly()
        {
            if( Age >= 10) Console.WriteLine("I am flying");
        }
    }

    public interface ILizard : ICreature
    {
        void Crawl()
        {
            if(Age < 10) Console.WriteLine("I am crawling");
        }
    }

    public class Dragon : Organism, IBird, ILizard
    {
        public int Age { get; set; }
    }

    public class Organism
    {
    }

    // ihneritance
    // SmartDregon(Dragon)
    // extension method
    // C#8 default interface methods
    
    class Program
    {
        static void Main(string[] args)
        {
            var d = new Dragon {Age = 5};
            
            // can cast
            ((ILizard)d).Crawl();
            
            // smart if
            if(d is ILizard lizard) lizard.Crawl();
            if(d is IBird bird) bird.Fly();
        }
    }
}