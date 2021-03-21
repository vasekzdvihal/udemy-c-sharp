using System;

namespace Decorator.StaticComposition
{
    public abstract class Shape
    {
        public string AsString();
    }
    
    public class TransparentShape : Shape
    {
        private Shape shape;
        private float transparency;

        public TransparentShape( Shape shape, float transparency)
        {
            this.shape = shape;
            this.transparency = transparency;
        }

        public string AsString() => $"{shape.AsString()} has {transparency * 100.00} transparency";
    }
    
    
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}