using System;

namespace Observer.EventKeyword
{
    public class FallsIllEventArgs 
    {
        public string Address;
    }
    
    public class Person
    {
        public void CatchACold()
        {
            FallsIll?.Invoke(this, new FallsIllEventArgs() { Address = "123 London Road"});
        }
        
        public event EventHandler<FallsIllEventArgs> FallsIll;
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            person.FallsIll += CallDoctor;
            person.CatchACold();
        }

        private static void CallDoctor(object? sender, FallsIllEventArgs e)
        {
            Console.WriteLine($"Doctor has been called to {e.Address}");
        }
    }
}