using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList list1 = new ArrayList();
            list1.Add(3);
            list1.Add(5);
            list1.Add(12);


            Console.WriteLine("Non-generic List resultrs...");
            foreach(int x in list1)
            {
                Console.WriteLine(x);
            }

            List<int> list2 = new List<int>();
            list2.Add(32);
            list2.Add(9);
            list2.Add(21);

            Console.WriteLine("Generic list results...");
            foreach(int item in list2)
            {
                Console.WriteLine(item);
            }
        }
    }
}
