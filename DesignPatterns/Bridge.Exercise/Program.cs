using System;

namespace Bridge.Exercise
{
    class Program
    {
        public interface IRenderer
        {
            string WhatToRenderAs { get; }            
        }

        public abstract class Shape
        {
            public string Name { get; set; }

            protected IRenderer Renderer;

            protected Shape(IRenderer renderer)
            {
                Renderer = renderer;
            }

            public override string ToString()
            {
                return $"Drawing {Name} as {Renderer.WhatToRenderAs}";
            }
        }

        public class Triangle : Shape
        {
            public Triangle(IRenderer renderer) : base(renderer)=> Name = "Triangle";
        }

        public class Square : Shape
        {
            public Square(IRenderer renderer) : base(renderer) => Name = "Square";
        }

        public class VectorRenderer : IRenderer
        {
            public string WhatToRenderAs
            {
                get { return "lines"; }
            }
        }

        public class RasterRenderer  : IRenderer
        {
            public string WhatToRenderAs
            {
                get { return "pixels"; }
            }
        }

        // imagine VectorTriangle and RasterTriangle are here too
        
        static void Main(string[] args)
        {
            IRenderer rasterRenderer = new RasterRenderer();
            var square = new Square(rasterRenderer);
            Console.WriteLine(square.ToString());
            
            IRenderer vectorRenderer = new VectorRenderer();
            var triangle = new Triangle(vectorRenderer);
            Console.WriteLine(triangle.ToString());
        }
    }
}