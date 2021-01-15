using System;

namespace Multicast_Delegate_Example
{
    class Program
    {
        delegate void del(int x, int y);

        static void Main(string[] args)
        {
            Mark m = new Mark();
            del d;

            d = m.AddNumbers;
            Console.WriteLine("Invoking delegate d with one target...");
            d(6, 5); 
            Console.WriteLine();

            d += m.MultipleNumbers;
            Console.WriteLine("Invoking delegate d with two target...");
            d(6, 5); 
            Console.WriteLine();

            d += m.SubtractNUmbers;
            Console.WriteLine("Invoking delegate d with three target...");
            d(6, 5);
            Console.WriteLine();

            d -= m.MultipleNumbers;
            Console.WriteLine("Invoking delegate d without Multiply...");
            d(6, 5);
            Console.WriteLine();
        }
    }

    public class Mark
    {
        public void AddNumbers(int a, int b)
        {
            Console.WriteLine("AddNumber: a+b= " + (a + b));
        }

        public void MultipleNumbers(int a, int b)
        {
            Console.WriteLine("MultipleNumbers: a*b= " + (a * b));
        }

        public void SubtractNUmbers(int a, int b)
        {
            Console.WriteLine("SubtractNumbers: a-b= " + (a - b));
        }
    }
}
