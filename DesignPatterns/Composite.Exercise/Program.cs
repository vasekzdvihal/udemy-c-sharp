using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Composite.Exercise
{
    public interface IValueContainer : IEnumerable<int>
    {
        
    }

    public class SingleValue : IValueContainer
    {
        public int Value;
        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {
      
    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
            foreach (var i in c)
                result += i;
            return result;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}