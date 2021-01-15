using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            double r = Math.Round(35.5);
            Console.WriteLine(r);

            Car.Accelerate(); 

            Car c = new Car();
            c.SlowDown();
        }
    }

    public class Car
    {
        public static void Accelerate()
        {
            Console.WriteLine("Car is accelerating...");
        }

        public void SlowDown()
        {
            Console.WriteLine("Car is slowing down...");
        }
    }
}
