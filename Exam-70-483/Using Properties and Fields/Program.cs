using System;

namespace Using_Properties_and_Fields
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            p.Age = 91;
            p.Weight = 75;

            Console.WriteLine("Age: " + p.Age);
        }
    }

    public class Person
    {
        public string Firstname { get; set; }
        private int weight;
        public int Weight
        {
            get { return Weight; }
            set { Weight = value; }
        }

        private int age;

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if((value > 0) && (value < 65))
                {
                    age = value;
                }
                else
                {
                    throw new Exception("Age cannot be over 65...");
                }
            }
        }
    }
}
