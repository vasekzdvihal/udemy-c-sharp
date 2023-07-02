# Delegates

## What is Delegate 

- Delegate is an object that knows how to call a method
- A delegate is a type (like a class)
    - It defines the method signature that can be called
    - A delegate considers the method signature to include the return type and the parameters types
- If we created a delegate type using this code

``` csharp
        delegate int d (int, int)
```

- we create a variable 'd' that is a type of delegate
- 'd' can now be used to cal any method that returns an int and has two int parameters

``` csharp
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    namespace Creating_A_Delegate
    {
    class Program
    {
    delegate int del(int x, int y);
    
            static void Main(string[] args)
            {
                Mark m = new Mark();
                del d = m.AddNumbers;
                Console.WriteLine(d(3, 4)); // 7
    
                d = m.MultiplyNumbers;
                Console.WriteLine(d(3, 4)); // 12
            }
        }
    
        public class Mark
        {
            public int AddNumbers(int a, int b)
            {
                return a + b;
            }
    
            public int MultiplyNumbers(int a, int b)
            {
                return a * b;
            }
        }
    }
```

## Multicast delegates

- All C# delegates have multicast capability
- A single delegate instance can reference more than one target method
- A target method can be added to a delegate instance using += and remove using -=
- Target methods are called in the order they were added to the delegate instance

``` csharp
    using System;
    
    namespace Multicast_Delegate_Example
    {
    class Program
    {
    delegate void del(int x, int y);
    
            static void Main(string[] args)
            {
                Mark m = new Mark();
                del d;
    
                d = m.AddNumbers;
                Console.WriteLine("Invoking delegate d with one target...");
                d(6, 5); 
                Console.WriteLine();
    
                d += m.MultipleNumbers;
                Console.WriteLine("Invoking delegate d with two target...");
                d(6, 5); 
                Console.WriteLine();
    
                d += m.SubtractNUmbers;
                Console.WriteLine("Invoking delegate d with three target...");
                d(6, 5);
                Console.WriteLine();
    
                d -= m.MultipleNumbers;
                Console.WriteLine("Invoking delegate d without Multiply...");
                d(6, 5);
                Console.WriteLine();
            }
        }
    
        public class Mark
        {
            public void AddNumbers(int a, int b)
            {
                Console.WriteLine("AddNumber: a+b= " + (a + b));
            }
    
            public void MultipleNumbers(int a, int b)
            {
                Console.WriteLine("MultipleNumbers: a*b= " + (a * b));
            }
    
            public void SubtractNUmbers(int a, int b)
            {
                Console.WriteLine("SubtractNumbers: a-b= " + (a - b));
            }
        }
    }
```