using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(Count);
            t.Start();

            Task task = Task.Run(() =>
            {
                for (int i = 0; i < 8; i++)
                {
                    Thread.Sleep(500);
                    Console.Write("BG ");
                }
            });
        }

        static void Count()
        {
            for(int i = 0; i < 4; i++)
            {
                Thread.Sleep(500);
                Console.Write("FG ");
            }
        }
    }
}
