using System;
using Autofac;

namespace Bridge
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }

    public abstract class Shape
    {
        protected IRenderer Renderer;

        protected Shape(IRenderer renderer)
        {
            Renderer = renderer;
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        private float radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            Renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // IRenderer renderer = new RasterRenderer();
            // var circle = new Circle(renderer, 5);
            //
            // circle.Draw();
            // circle.Resize(2);
            // circle.Draw();
            //
            // IRenderer vectorRenderer = new VectorRenderer();
            // var vectorCircle = new Circle(vectorRenderer, 15);
            //
            // vectorCircle.Draw();
            // vectorCircle.Resize(5);
            // vectorCircle.Draw();

            var cb = new ContainerBuilder();
            cb.RegisterType<VectorRenderer>().As<IRenderer>()
                .SingleInstance();
            cb.Register((c, p) =>
                new Circle(c.Resolve<IRenderer>(),
                    p.Positional<float>(0)));

            using (var c = cb.Build())
            {
                var circle = c.Resolve<Circle>(new PositionalParameter(0, 5.0f));
                
                circle.Draw();
                circle.Resize(2);
                circle.Draw();
            }
        }
    }
}