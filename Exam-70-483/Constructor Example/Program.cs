using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            DefConstructor dc = new DefConstructor();
            dc.DoThis();

            AddConstructor ac = new AddConstructor();
        }
    }

    public class DefConstructor
    {
        public void DoThis()
        {
            Console.WriteLine("DoThis has run...");
        }
    }

    public class AddConstructor
    {
        public AddConstructor()
        {
            Console.WriteLine("The constructor has been caled...");
            DoThat();
        }

        public void DoThat()
        {
            Console.WriteLine("DoThat has executed...");
        }
    }
}
