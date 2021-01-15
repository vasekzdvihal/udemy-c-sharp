using System;

namespace InhertitanceExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Patient p = new Patient();
            p.Examine("Jamison");

            Child c = new Child();
            c.Examine("Bobby");
            c.Inoculate();

            UnderFive uf = new UnderFive();
            uf.Examine("Jessie");
            uf.UnderFiveMethod();
        }
    }

    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public long SSN { get; set; }

        public void Examine(string pname)
        {
            Console.WriteLine("Examination of " + pname + " completed.");
        }

        public void Billing(long ssn)
        {
            Console.WriteLine("BIlling compleed...");
        }
    }

    public class Child : Patient
    {
        public void Inoculate()
        {
            Console.WriteLine("Child has been incolute...");
        }
    }

    public class UnderFive : Child
    {
        public void UnderFiveMethod()
        {
            Console.WriteLine("UnderFive method has been called...");
        }
}
}
