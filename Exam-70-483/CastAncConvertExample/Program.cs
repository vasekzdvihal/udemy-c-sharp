using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastAncConvertExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 7;
            double y = 12.6;
            x = (int)y;
            Console.WriteLine("Double casted to int: " + x); // 12

            int xx = 7;
            double yy = 12.6;
            xx = Convert.ToInt32(yy);
            Console.WriteLine("Double converted to int: " + xx); // 13

            string str = "123";
            int r;
            r = Convert.ToInt32(str);
            Console.WriteLine("Using Convert to convert string to int: " + r); // 123
        }
    }
}
 