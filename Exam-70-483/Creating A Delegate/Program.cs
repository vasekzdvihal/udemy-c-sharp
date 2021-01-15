using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creating_A_Delegate
{
    class Program
    {
        delegate int del(int x, int y);

        static void Main(string[] args)
        {
            Mark m = new Mark();
            del d = m.AddNumbers;
            Console.WriteLine(d(3, 4)); // 7

            d = m.MultiplyNumbers;
            Console.WriteLine(d(3, 4)); // 12
        }
    }

    public class Mark
    {
        public int AddNumbers(int a, int b)
        {
            return a + b;
        }

        public int MultiplyNumbers(int a, int b)
        {
            return a * b;
        }
    }
}
