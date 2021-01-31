using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Exercise
{
    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point other)
        {
            X = other.X;
            Y = other.Y;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line(Point start,Point end)
        {
            Start = new Point(start);
            End = new Point(end);
        }

        public Line DeepCopy()
        {
            return new Line(Start, End);
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var line = new Line(new Point(1, 1), new Point(45, 45));
            var line2 = line.DeepCopy();

            line2.Start.X = 1213213;
            line2.Start.Y = 456463213;

            Console.WriteLine(line);
            Console.WriteLine(line2);
        }
    }
}
