using System;

namespace Exception_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 4;
            int b = 0;

            try
            {
                Console.WriteLine("a divided by b is: " + a / b);
            }
            catch(Exception e)
            {
                Console.WriteLine("The following error has occured: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Finally has been called...");
            }

            Console.WriteLine("... the program continues...");
        }
    }
}
  