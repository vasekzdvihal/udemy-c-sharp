using System;
using System.IO;
using System.Text;

namespace StringWriter_And_StringReader
{
    class Program
    {
        static void Main(string[] args)
        {
            StringReadWrite srw = new StringReadWrite();
        }
    }

    #region "Write data using StringWriter"
    public class StringReadWrite
    {
        StringBuilder sb = new StringBuilder();

        public StringReadWrite()
        {
            WriteData();
            ReadData();
        }

        public void WriteData()
        {
            StringWriter sw = new StringWriter(sb);

            Console.WriteLine("Please enter you first and last name...");
            string name = Console.ReadLine();

            sw.WriteLine("Name: " + name);
            sw.Flush();
            sw.Close();
        }
        #endregion
        #region "Read data from the StringBuilder object"
        public void ReadData()
        {
            StringReader sr = new StringReader(sb.ToString());

            Console.WriteLine("Reading the information...");

            while(sr.Peek() > -1)
            {
                Console.WriteLine(sr.ReadLine());
            }

            Console.WriteLine();
            Console.WriteLine("Thank you!");

            sr.Close();
        }
        #endregion
    }
}
