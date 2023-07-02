# Implementing Class Hierarchies

## The magic if inheritance

- Inheritance is one of the three pillars of object oriented programming
- Inheritance enables you to create new classes that reuse, extend and modify the behaviour that is defined in other
  classes
- Writing some code in two different classes is going to become problem
    - Changes in properties or methods must be made in two classes
    - What if we could't share the some functionality between the two classes
- Inheritance provides the ability to write code once and use it in many different locations
    - It also adds a few design options and considerations

## Using Inheritance

- Inheritance provides a number of efficiencies
    - It also provides a few design considerations
- Inheritance is usually implemented in a general down to specific model
- There are a few terms you should be comfortable with
    - Patient is the base class
    - Adult is a subclass that inherits from the "parent" class
    - "Child" is also referred to as a derived class
- A class can only inherit from one class
    - Adult cannot inherit from "parent" and another class
    - More than one class can inherit from "parent"
    - Inheritance is transitive
    - A class inheriting from "child" also has access to properties and methods in "parent"

``` csharp    
    using System;

    namespace InhertitanceExample {
      class Program {
        static void Main(string[] args) {
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

      public class Patient {
        public string FirstName {
          get;
          set;
        }
        public string LastName {
          get;
          set;
        }
        public int Age {
          get;
          set;
        }
        public int Weight {
          get;
          set;
        }
        public long SSN {
          get;
          set;
        }

        public void Examine(string pname) {
          Console.WriteLine("Examination of " + pname + " completed.");
        }

        public void Billing(long ssn) {
          Console.WriteLine("BIlling compleed...");
        }
      }

      public class Child: Patient {
        public void Inoculate() {
          Console.WriteLine("Child has been incolute...");
        }
      }

      public class UnderFive: Child {
        public void UnderFiveMethod() {
          Console.WriteLine("UnderFive method has been called...");
        }
      }
    }
```

## Understanding Overloading

- A method is a code block that contains a series of statements
- Every method has a 'signature'
    - Just like your signature, it is unique to the method
    - Can be used to identify the method
- Multiple methods can be created using the same name
    - As long as the signatures are unique
- We can 'overload' methods based on their unique signatures
    - For the purpose of overloading a method's signature is it's name and the parameters it accepts
    - public string AddRecord(int a, string b)

``` csharp    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    namespace Overloading_Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                Patient p = new Patient();
                p.CheckBilling();
                p.CheckBilling(1, 1);
                p.CheckBilling("Mark", 1);
    
                Child c = new Child();
                c.CheckBilling("b", "c");
            }
        }
    
        public class Patient
        {
            public void CheckBilling()
            {
                Console.WriteLine("Patient: The billing has been checked...");
            }
    
            public void CheckBilling(int a, int b)
            {
                Console.WriteLine("Patient: The billing has been checked using two submitted integers...");
            }
    
            public void CheckBilling(string a, int b)
            {
                Console.WriteLine("Patient: The billing has been checked using a submitted string and integer...");
            }
        }
    
        public class Child : Patient
        {
            public void CheckBilling(string a, string b)
            {
                Console.WriteLine("Patient: The billing has been checked using two submitted strings...");
    
            }
        }
    }
```

## Overriding Methods

- Inheritance allows the Adult class to utilise the Examine method in the Patient class
    - What if we add an Examine method to the Adult class that provides different functionality from the Patient class?
    - How do we use the Examine method in the Adult class?
- And override method provides a new implementation of a member that is inherited from a base class
- Overriding a method requires the use of two keywords
    - The base method that is being replaced or overridden must include to keyword virtual
    - The overriding method must include the keyword override 

``` csharp
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
```

## Extension methods

- Sometimes you'd like to add a particular functionality to a class that already exists
- Extensions methods provide a way to do this
    - Without creating a new derived type
    - Without recompiling
    - Without modifying the original type
- An extension method is actually static method in a static class
    - The 'this' modifier is applied to the first method parameter
    - The type of the first parameter is the type that will be extended

``` csharp
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    namespace Extension_Method_Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                string x = "Hello";
                string y = "he llo";
    
                Console.WriteLine(x + " is capitalized? " + x.IsCap());
                Console.WriteLine(y + " is capitalized? " + y.IsCap());
            }
        }
    
        public static class StringCap
        {
            public static bool IsCap(this string s)
            {
                if (string.IsNullOrEmpty(s)) return false;
                return char.IsUpper(s[0]);
            }
        }
    }
```


## Understanding Interfaces

- What is Interface?
    - A contract that requires a class to implement specific functionalities
- What interfaces accomplish?
    - Consistent functionality
    - Polymorphism
- An interface in C# provides similar functionality for our classes
    - It defines what functionalities a class must provide
    - It does not provide the specific functionalities
- One important note about interfaces...
    - Classes cannot inherit from more than one class
    - Classes can implement more that one interface

## Creating an Interface

- An interface contains definitions for a group of related functionalities that a class or struct must provide
    - SW version of contract
- An interface tells a class what it must do
    - It does not tell the class how to do it
- What can an interface define
    - Methods
    - Properties
    - Events
    - Indexers
- Interface members are automatically public
- Interface members can't include any access modifiers
- A class implement an interface
    - It agreed's to the terms of the contract
    - It must provide an implementation for all members defined in the interface

``` csharp
    namespace Creating_an_Interface
    {
        class Program
        {
            static void Main(string[] args)
            {
                Machine1 m1 = new Machine1();
                Machine2 m2 = new Machine2();
                Machine3 m3 = new Machine3();
    
                m1.Start();
                m1.Stop();
                m2.Start();
                m2.Stop();                
                m3.Start();
                m3.Stop();
            }
        }
    
        public class Machine1 : IControls
        {
            public void Start()
            {
                // add code
            }
    
            public void Stop()
            {
                // add code
            }
        }
    
        public class Machine2 : IControls
        {
            public void Start()
            {
                // add code
            }
    
            public void Stop()
            {
                // add code
            }
        }
    
        public class Machine3 : IControls
        {
            public void Start()
            {
                // add code
            }
    
            public void Stop()
            {
                // add code
            }
        }
    
        interface IControls
        {
            void Start();
            void Stop();
        }
    }
```

## Explicit Interface Implementation

- C# does not allow multiple inheritance
- C# does allow a class to implement multiple interfaces
- Explicit interface implementation can be utilised to resolve the problem

``` csharp
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    namespace Explicit_Implementation
    {
        class Program
        {
            static void Main(string[] args)
            {
                Mark m = new Mark();
                m.DoThis();
    
                ((ISecond)m).DoThis();
    
                ISecond mm = new Mark();
                mm.DoThis();
            }
        }
    
        interface IFirst
        {
            void DoThis();
        }
    
        interface ISecond
        {
            int DoThis();
        }
    
        public class Mark : IFirst, ISecond
        {
            public void DoThis()
            {
                Console.WriteLine("Implmentation of IFirst.DoThis...");
            }
    
            int ISecond.DoThis()
            {
                Console.WriteLine("Implementation of ISecond.DoThis...");
                return 6;
            }
        }
    }
```

## IEnumerable

- Computers programs deal with various collections of data
    - Simple data structures
    - Arrays
    - Lists
    - Etc...
- The internals of these collections may be very different
- The need to traverse the contents of these collections is universal
    - Locate items, populate controls, etc...
- C# provides the foreach construct to make collection iteration simple across various collection types
- The .NET Framework provides two interfaces to provide a standardised method of accessing collection data
    - IEnumerable
    - IEnumerator
- Note that since we are using interfaces, the code for providing enumeration resides in a separate class
    - Multiple enumerator objects can run simultaneously
- The IEnumerable interface defines one method for iterating through a collection
    - public IEnumerator GetEnumerator()
    - Note that GetEnumerator() appears to return on enumerator
- The IEnumerator interface defines three methods that provide the functionality to iterate through a collection
    - bool MoveNext()
    - object Current { get; }
    - void Reset();
- Implementing IEnumerable requires you to provide an enumerator
- There are three ways to accomplish this
    - If the class is 'wrapping' another collection, you can return the wrapped collection's enumerator
    - Creating your own complete IEnumerator implementation

## Implementing IEnumerable

**The IEnumerable interface defines a single method that returns an IEnumerator object**

- public IEnumerator GetEnumerator()
- To implement IEnumerable, we need to provide functionality for the GetEnumerable method
    - We need to write an iterator that moves through the collection and returns each item
- The 'yield return' statement provides the functionality we need to simplify the process
    - Yield return is a compiler instruction
    - It causes the compiler to create a hidden enumerator class behind the scenes
    - If then refactors GetEnumerator() to instantiate and return that class

``` csharp
    using System;
    using System.Collections;
    using System.Collections.Generic;
    
    namespace Implementating_IEnumerable
    {
        class Program
        {
            static void Main(string[] args)
            {
                List<string> cars = new List<string>();
                cars.Add("Chevrolet");
                cars.Add("Honda");
                cars.Add("Lexus");
                cars.Add("Ford");
    
                foreach(string car in cars)
                {
                    Console.WriteLine(car);
                }
    
                Console.WriteLine("\n" + "Now our own collection..." + "\n");
    
                Car c = new Car();
                c[0] = "Chevrolet";
                c[1] = "Mercedes";
                c[2] = "Dodge";
    
                foreach(string car in c)
                {
                    Console.WriteLine(car);
                }
            }
        }
    
        public class Car : IEnumerable
        {
            string[] car = new string[3];
            
            public string this[int carNum]
            {
                get { return car[carNum]; }
                set { car[carNum] = value; }
            }
    
            public IEnumerator GetEnumerator()
            {
                foreach(string c in car)
                {
                    yield return c;
                }
            }
        }
    }
```