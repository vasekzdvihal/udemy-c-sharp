using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explicit_Implementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Mark m = new Mark();
            m.DoThis();

            ((ISecond)m).DoThis();

            ISecond mm = new Mark();
            mm.DoThis();
        }
    }

    interface IFirst
    {
        void DoThis();
    }

    interface ISecond
    {
        int DoThis();
    }

    public class Mark : IFirst, ISecond
    {
        public void DoThis()
        {
            Console.WriteLine("Implmentation of IFirst.DoThis...");
        }

        int ISecond.DoThis()
        {
            Console.WriteLine("Implementation of ISecond.DoThis...");
            return 6;
        }
    }
}
