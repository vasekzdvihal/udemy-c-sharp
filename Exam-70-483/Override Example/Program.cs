using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Override_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Patient p = new Patient();
            p.Examine();

            Adult a = new Adult();
            a.Examine();
        }
    }

    public class Patient
    {
        public virtual void Examine()
        {
            Console.WriteLine("The Patient has been examined...");
        }
    }

    public class Adult : Patient
    {
        public override void Examine()
        {
            Console.WriteLine("The Adult has been examined...");
        }
    }
}
