using System;
using System.Collections.Generic;

namespace Collection_and_Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a collection that is a list of strings
            var dogs = new List<string>();

            dogs.Add("Bulldog");
            dogs.Add("Collie");
            dogs.Add("Retriever");

            // foreach to move through the list
            foreach(var dog in dogs)
            {
                Console.WriteLine(dog + " ");
            }

            Console.WriteLine(dogs[1]);

            // Create a array of integers
            int[] a1 = new int[] { 1, 3, 5, 7, 9, 11 };

            foreach(int i in a1)
            {
                Console.WriteLine(i + " ");
            }
        }
    }
}
