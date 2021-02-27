using System;

namespace Decorator.MultipleInheritance
{
    class Program
    {
        public interface IBird
        {
            void Fly();
            int Weight { get; set; }
        }

        public class Bird : IBird
        {
            public int Weight { get; set; }
            
            public void Fly()
            {
                Console.WriteLine($"Soaring in the sky with weight {Weight}");
            }
        }

        public interface ILizard
        {
            void Crawl();
            int Weight { get; set; }
        }

        public class Lizard : ILizard
        {
            public int Weight { get; set; }
            
            public void Crawl()
            {
                Console.WriteLine($"Crawling in the dirt with weight {Weight}");
            }
        }

        public class Dragon : ILizard, IBird
        {
            private Bird bird = new Bird();
            private Lizard lizard = new Lizard();
            private int weight;

            public void Fly()
            {
                bird.Fly();
            }

            public void Crawl()
            {
                lizard.Crawl();
            }

            public int Weight
            {
                get { return weight; }
                set
                {
                    weight = value;
                    bird.Weight = value;
                    lizard.Weight = value;
                }
            }
        }
        
        static void Main(string[] args)
        {
            var d = new Dragon();
            d.Weight = 15477;
            d.Fly();
            d.Crawl();
        }
    }
}