using System;
using System.Text;

namespace String_Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = "This is an example of ";
            s1 = s1 + "string concatenation";
            Console.WriteLine(s1);

            StringBuilder sb2 = new StringBuilder("This is an example of ");
            sb2.Append("string concatenation");
            Console.WriteLine(sb2);

            sb2.AppendLine();
            sb2.Append(" using StringBuilder...");
            Console.WriteLine(sb2);

            sb2.Replace("StringBuilder", "the StringBuilder class");
            Console.WriteLine(sb2); 
        }
    }
}
