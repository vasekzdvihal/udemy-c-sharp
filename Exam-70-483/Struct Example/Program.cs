using System;

namespace Struct_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Mark m = new Mark(6, 7);
            m.DoThis();

            John j;
            j.x = 15;
            j.y = 10;
            j.DoThat();
        }
    }

    public struct Mark
    {
        public int x;
        public int y;

        public Mark(int a, int b)
        {
            x = a;
            y = b;
        }

        public void DoThis()
        {
            Console.WriteLine(x + y);
        }
    }

    public struct John
    {
        public int x;
        public int y;

        public void DoThat()
        {
            Console.WriteLine(x + y);
        }
    }
}
