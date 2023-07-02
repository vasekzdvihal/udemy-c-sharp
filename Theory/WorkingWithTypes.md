# Working with C# Types

### Properties and Fields

- Properties and fields provides objects the ability to store data
    - Consider a Person object
        - Gender
        - Weight
        - Age
    - These are often called "properties"
- How should we provide the ability to set and change this data?
    - Uncontrolled access to data can create serious issues
    - Invalid values can cause errors
    - Invalid values can create security concerns
- Encapsulation solves the problem
    - And explains the difference between properties and fields
    - This principle states that data stored inside an object
    - Not directly publicly accessible from outside the objects
- Fields provide the ability to store data privately
    - A field is only available from within the object
- Properties provide the ability to read and write data to the private fields
    - Specifically, they provide encapsulation for the fields
    - Data being written to or read from a field can be verified, modified, etc...
- Properties look and feel like public data members
    - They are actually special methods called accessors
- Here's the short version...
    - Fields is a private variable that can only be accessed by code within the object
    - A property is used to provide public access to the private field

``` csharp
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
```

## Methods

### Public/Private and more

- A method is a named code block that carries out a series of statements
    - A program causes the statement to be executed by calling the method and specifying an required method arguments
    - Similar to subroutines in other languages
    - In C#, every executed instruction is performed in the context of method
- Methods have a few characteristics you should understand

#### Access modifier

- Controls the availability of the method to other code
- See 'Access modifiers'

#### Return type

- Method can return a value to the calling code
- Sometimes called a function when its return a value
- If the return type is not 'void' the method will return a value
- A 'return' statement inside the method will stop execution and return a value to the caller of the value matches the
  return type

#### Parameters

- Specifies the name and data type of values that must be provided when the methods is called
- The values passed to the methods parameters are called arguments
- The arguments must be compatible with the parameter data type 

#### Signature

- Refers to the structure if the method
- For overloading, the signature includes the method name and the parameter types
- For delegates, the signature includes the return type and the parameter types

### Access modifiers

- All types and members have an accessibility level
    - Controls whether they be used from other code within an assembly or the other assembly
- Five accessibility levels

1. PUBLIC
    - The type or member can be accessed by any other code in the same assembly or another assembly that references it
2. PRIVATE
    - The type or member can be accessed only by code in the same class or struct
3. PROTECTED
    - The type or member can be accessed only by code in the same class or struct, or in a class that is derived from
      that class
4. INTERNAL
    - The type or member can be accessed by any code in the same assembly, but not from another assembly
5. PROTECTED INTERNAL
    - The type or member can be accessed by any code in the assembly in which it is declare...
    - Or from within a derived class in another assembly

### Named and optional arguments

- NOTE: The words 'parameters' and 'arguments ' are often used interchangeably
- A method's parameters are usually utilised positionally

``` csharp
    NewInfo("Mark", 39);


    public int NewInfo(string fname, int age)
    {
        // code
    }
```

- Named argument ca be used instead of positional

``` csharp
    NewInfo(age: 39, fname: "Mark");
```

- By default, all parameters listed are required when calling the method
- Optional parameters can be defined
    - Each optional parameter has default value as part of it's definition
    - If no arguments is sent for that parameter, the default value is used
- Optional parameters are defined at the end if the parameter list , after any required parameters

``` csharp
    public int NewInfo(string fname, int age = 39)
    {
        // code
    }
```

## Understanding constructors

- Every class or struct has a constructor
- What is a constructor?
    - A method that executed automatically every time an object is instantiated from the class
    - A method with same name as the class
    - If a constructor is not provided by the programmer, C# creates a default constructor
- What does a constructor do?
    - Enables actions to be taken as an object is created based on the class
    - Set default values
    - Limit instantiation
    - Take a specific action
    - Etc...
- The are three main types of constructors

1. INSTANCE
    - Used to create and initialise any instance member variables when you use the new expression to create an object of
      a class
2. PRIVATE
    - Prevents other classes from creating instances of the class
    - Only nested classes can create an instance of this class
3. STATIC
    - Used to initialise an static data
    - A static constructor is called only once before the first instance of the class is created or any static members
      are referenced
4. CONSTRUCTORS CAN BE OVERLOADED!

``` csharp
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    namespace Constructor_Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                DefConstructor dc = new DefConstructor();
                dc.DoThis();
    
                AddConstructor ac = new AddConstructor();
            }
        }
    
        public class DefConstructor
        {
            public void DoThis()
            {
                Console.WriteLine("DoThis has run...");
            }
        }
    
        public class AddConstructor
        {
            public AddConstructor()
            {
                Console.WriteLine("The constructor has been caled...");
                DoThat();
            }
    
            public void DoThat()
            {
                Console.WriteLine("DoThat has executed...");
            }
        }
    }
```

*Output:*

``` csharp
    DoThis has run...
    The constuctor has been caled...
    DoThat has executed...
    Press any key to continue...
```

### Understanding Static

- The static modifier changes the behaviour of C# types and members
- What's the difference?
    - A static class or member cannot be accessed through an instance
- Classes and methods can be marked as static
    - In static class, all methods must be marked as static
- A non-static class is used to create multiple objects based on the non-static type
    - Objects are created using the 'new' keyword
- You cannot use the 'new' keyword with a static class
    - Only one instance of the object ever exists

``` csharp
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    namespace Static_Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                double r = Math.Round(35.5);
                Console.WriteLine(r);
    
                Car.Accelerate(); 
    
                Car c = new Car();
                c.SlowDown();
            }
        }
    
        public class Car
        {
            public static void Accelerate()
            {
                Console.WriteLine("Car is accelerating...");
            }
    
            public void SlowDown()
            {
                Console.WriteLine("Car is slowing down...");
            }
        }
    }
```

### Understanding Structs

- A structs is very similar to a class
    - It looks and acts like a class
    - But is a VALUE TYPE, not a reference type
- Structs shore many functionalities of classes
    - Fields, properties, methods and events
    - Can implement Interfaces
    - Instanced with the 'new' keyword
- How structs are different from classes
    - Cannot declare a default constructor (constructor with no parameters)
    - Cannot inherit from another struct or class
    - Can be instanced without the 'new' keyword but there is no constructor call and all fields must be initialised
      before the object can be used

``` csharp
    using System;
    
    namespace Struct_Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                Mark m = new Mark(6, 7);
                m.DoThis();
    
                John j;
                j.x = 15;
                j.y = 10;
                j.DoThat();
            }
        }
    
        public struct Mark
        {
            public int x;
            public int y;
    
            public Mark(int a, int b)
            {
                x = a;
                y = b;
            }
    
            public void DoThis()
            {
                Console.WriteLine(x + y);
            }
        }
    
        public struct John
        {
            public int x;
            public int y;
    
            public void DoThat()
            {
                Console.WriteLine(x + y);
            }
        }
    }
```

### Generic types

- What are generic?
    - Collections that are type-safe at compile time
    - A faster, safer, more efficient way to deal with collections
    - Introduced in C# version 2.0
    - Make it possible to design classes and methods that defer the specification of one or more types until the class
      or method is declared and instantiated by client code
    - Generics provide the solution to a limitation in earlier versions of the common language runtime and the C#
      language
    - Prior to C# version 2.0, collections such as ArrayList were non-generics
    - Generalisation was accomplished by casting types to and from the universal base type Object
- Why do we need generics ?
    - Items (value or reference types) that were added to an ArrayList were implicitly upcast to Objects
    - Value types require boxing to be added and unboxing when retrieved
    - Boxing can seriously affect performance

``` csharp
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    namespace Generics_Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                ArrayList list1 = new ArrayList();
                list1.Add(3);
                list1.Add(5);
                list1.Add(12);
    
    
                Console.WriteLine("Non-generic List resultrs...");
                foreach(int x in list1)
                {
                    Console.WriteLine(x);
                }
    
                List<int> list2 = new List<int>();
                list2.Add(32);
                list2.Add(9);
                list2.Add(21);
    
                Console.WriteLine("Generic list results...");
                foreach(int item in list2)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
```

**Casting and converting**

- C# variables are statically typed
    - Data types are assigned to variables and the data types are checked at compile time
- This type-checking helps prevents bugs
- There are many instances where a variable of a specific type needs to store data of a different type
- Casting and converting are the processes provided to change the data type
- There are two main kinds of type conversions in C#
    - IMPLICIT
        - These conversions happens automatically because there is no risk of data loss
        - A variable of type long (8-long integer) can store any value that on int (4 bytes) can store
    - EXPLICIT
        - If the conversion cannot be made without the risk of losing information (larger to smaller data type), an
          explicit conversion is required
        - Called a cast
        - Informs the compiler that you understand that data loss may occur
- C# also provides the convert class for converting data types
    - Convert provides similar functionality but gets a little more complex under the hood
    - Convert is great when working with strings
    - Simplest answer: Try cast, if it fails, try convert 

``` csharp
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    namespace CastAncConvertExample
    {
        class Program
        {
            static void Main(string[] args)
            {
                int x = 7;
                double y = 12.6;
                x = (int)y;
                Console.WriteLine("Double casted to int: " + x); // 12
    
                int xx = 7;
                double yy = 12.6;
                xx = Convert.ToInt32(yy);
                Console.WriteLine("Double converted to int: " + xx); // 13
    
                string str = "123";
                int r;
                r = Convert.ToInt32(str);
                Console.WriteLine("Using Convert to convert string to int: " + r); // 123
            }
        }
    }
```

### Boxing and unboxing

- C# is based on a unified type system
    - Everything can be an object
    - But there are two main types, value and references
    - Value and reference types are stored and managed differently
    - Boxing provides a common way to handle both types
- Example:
    - A collection of objects has been created
    - Boxing allows this
    - The integer variable is wrapped in a System.Object
    - Now it is stored on the heap as a reference type and not on the stack as value type
        - Boxing is an implicit conversion
        - Unboxing is an explicit conversion
        - Excessive boxing can get expensive

``` csharp
    using System;
    
    namespace BoxingAndUnboxing
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Hello World!");
            }
        }
    }
```

### Indexers

An indexers provides the ability to access a type using on index similar to an array

``` csharp
    using System;
    
    namespace Indexer_Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                Car c = new Car();
                c[1] = "Mercedes";
                c[2] = "Dodge";
                c[3] = "Chevrolet";
                c[4] = "Honda";
                 
                Console.WriteLine("Car 1 is: " + c[1]); // Mercedes
                Console.WriteLine("Car 2 is: " + c[2]); // Dodge
                Console.WriteLine("Car 4 is: " + c[41]); // Out of index range...
            }
        }
    
        public class Car
        {
            string[] car = new string[40];
    
            public string this[int carNum]
            {
                get
                {
                    if(carNum>= 0 && carNum < car.Length)
                    {
                        return car[carNum];
                    }
                    return "Out of index range...";
                }
    
                set
                {
                    if(carNum >= 0 && carNum < car.Length)
                    {
                        car[carNum] = value;
                    }
                }
            }
        }
    }
```