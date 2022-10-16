# SOLID Principle

## Single Responsibility Principle

- A class should only have one reason to change
  - Separation of concerns - different classes handling different, independent task/problems


      using System;
      using System.Collections.Generic;
      using System.Diagnostics;
      using System.IO;
      using static System.Console;
      namespace DotNetDesignPatternDemos.SOLID.SRP {
        // just stores a couple of journal entries and ways of
        // working with them
        public class Journal {
          private readonly List < string > entries = new List < string > ();
          private static int count = 0;

          public int AddEntry(string text) {
            entries.Add($"{++count}: {text}");
            return count; // memento pattern!
          }

          public void RemoveEntry(int index) {
            entries.RemoveAt(index);
          }

          public override string ToString() {
            return string.Join(Environment.NewLine, entries);
          }

          // breaks single responsibility principle
          public void Save(string filename, bool overwrite = false) {
            File.WriteAllText(filename, ToString());
          }

          public void Load(string filename) {}
          public void Load(Uri uri) {

          }
        }

        // handles the responsibility of persisting objects
        public class Persistence {
          public void SaveToFile(Journal journal, string filename, bool overwrite = false) {

            if (overwrite || !File.Exists(filename))
              File.WriteAllText(filename, journal.ToString());
          }
        }

        public class Demo {
          static void Main(string[] args) {
            var j = new Journal();
            j.AddEntry("I cried today.");
            j.AddEntry("I ate a bug.");
            WriteLine(j);
            var p = new Persistence();
            var filename = @ "c:\temp\journal.txt";
            p.SaveToFile(j, filename);
            Process.Start(filename);
          }
        }
      }

## Open Closed Principle

Classes should be open for extension but closed for modification

    using System;
    using System.Collections.Generic;
    using static System.Console;
    namespace DotNetDesignPatternDemos.SOLID.OCP {
      public enum Color {
        Red,
        Green,
        Blue
      }

      public enum Size {
        Small,
        Medium,
        Large,
        Yuge
      }

      public class Product {
        public string Name;
        public Color Color;
        public Size Size;
        public Product(string name, Color color, Size size) {
          Name = name ??
            throw new ArgumentNullException(paramName: nameof(name));
          Color = color;
          Size = size;
        }
      }

      public class ProductFilter {
        // let's suppose we don't want ad-hoc queries on products
        public IEnumerable < Product > FilterByColor(IEnumerable < Product > products, Color color) {
          foreach(var p in products)
          if (p.Color == color)
            yield
          return p;
        }

        public static IEnumerable < Product > FilterBySize(IEnumerable < Product > products, Size size) {
          foreach(var p in products)
          if (p.Size == size)
            yield
          return p;
        }

        public static IEnumerable < Product > FilterBySizeAndColor(IEnumerable < Product > products, Size size, Color color) {
          foreach(var p in products)
          if (p.Size == size && p.Color == color)
            yield
          return p;
        } // state space explosion 
        // 3 criteria = 7 methods 
        // OCP = open for extension but closed for modification
      }

      // we introduce two new interfaces that are open for extension
      public interface ISpecification < T > {
        bool IsSatisfied(Product p);
      }

      public interface IFilter < T > {
        IEnumerable < T > Filter(IEnumerable < T > items, ISpecification < T > spec);
      }

      public class ColorSpecification: ISpecification < Product > {
        private Color color;
        public ColorSpecification(Color color) {
          this.color = color;
        }
        public bool IsSatisfied(Product p) {
          return p.Color == color;
        }
      }

      public class SizeSpecification: ISpecification < Product > {
        private Size size;
        public SizeSpecification(Size size) {
          this.size = size;
        }
        public bool IsSatisfied(Product p) {
          return p.Size == size;
        }
      }

      // combinator
      public class AndSpecification < T >: ISpecification < T > {
        private ISpecification < T > first, second;

        public AndSpecification(ISpecification < T > first, ISpecification < T > second) {
          this.first = first ??
            throw new ArgumentNullException(paramName: nameof(first));
          this.second = second ??
            throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(Product p) {
          return first.IsSatisfied(p) && second.IsSatisfied(p);
        }
      }

      public class BetterFilter: IFilter < Product > {
        public IEnumerable < Product > Filter(IEnumerable < Product > items, ISpecification < Product > spec) {
          foreach(var i in items)
          if (spec.IsSatisfied(i))
            yield
          return i;
        }
      }

      public class Demo {
        static void Main(string[] args) {
          var apple = new Product("Apple", Color.Green, Size.Small);
          var tree = new Product("Tree", Color.Green, Size.Large);
          var house = new Product("House", Color.Blue, Size.Large);
          Product[] products = {
            apple,
            tree,
            house
          };
          var pf = new ProductFilter();
          WriteLine("Green products (old):");
          foreach(var p in pf.FilterByColor(products, Color.Green))
          WriteLine($" - {p.Name} is green");
          // ^^ BEFORE
          // vv AFTER
          var bf = new BetterFilter();
          WriteLine("Green products (new):");
          foreach(var p in bf.Filter(products, new ColorSpecification(Color.Green)))
          WriteLine($" - {p.Name} is green");
          WriteLine("Large products");
          foreach(var p in bf.Filter(products, new SizeSpecification(Size.Large)))
          WriteLine($" - {p.Name} is large");
          WriteLine("Large blue items");
          foreach(var p in bf.Filter(products,
            new AndSpecification < Product > (new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large)))) {
            WriteLine($" - {p.Name} is big and blue");
          }
        }
      }
    }

## Liskov Substitution Principle

         using static System.Console;
        namespace Solid.LiskovSubstitution {
          // using a classic example
          public class Rectangle {

            public virtual int Width {
              get;
              set;
            }

            public virtual int Height {
              get;
              set;
            }

            public Rectangle() {}

            public Rectangle(int width, int height) {
              Width = width;
              Height = height;
            }

            public override string ToString() {
              return $ "{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
          }

          public class Square: Rectangle {
            public override int Width // nasty side effects
            {
              set {
                base.Width = base.Height = value;
              }
            }

            public override int Height {
              set {
                base.Width = base.Height = value;
              }
            }
          }

          public class Demo {
            static public int Area(Rectangle r) => r.Width * r.Height;
            static void Main(string[] args) {
              Rectangle rc = new Rectangle(2, 3);
              WriteLine($"{rc} has area {Area(rc)}");
              // should be able to substitute a base type for a subtype
              /*Square*/
              Rectangle sq = new Square();
              sq.Width = 4;
              WriteLine($"{sq} has area {Area(sq)}");
            }
          }
        }

### Interface Segregation Principle

- Don't put too much into an interface; split into separate interfaces
- YAGNI - You Ain't Going to Need It

      using System;
      namespace Solid.InterfaceSegregation {
        public class Document {}
        public interface IMachine {
          void Print(Document d);
          void Fax(Document d);
          void Scan(Document d);
        }
        // ok if you need a multifunction machine
        public class MultiFunctionPrinter: IMachine {
          public void Print(Document d) {
            //
          }
- 
          public void Fax(Document d) {
            //
          }
- 
          public void Scan(Document d) {
            //
          }
        }
        public class OldFashionedPrinter: IMachine {
          public void Print(Document d) {
            // yep
          }
- 
          public void Fax(Document d) {
            throw new System.NotImplementedException();
          }
- 
          public void Scan(Document d) {
            throw new System.NotImplementedException();
          }
        }
- 
        public interface IPrinter {
          void Print(Document d);
        }
- 
        public interface IScanner {
          void Scan(Document d);
        }
- 
        public class Printer: IPrinter {
          public void Print(Document d) {}
        }
- 
        public class Photocopier: IPrinter, IScanner {
          public void Print(Document d) {
            throw new System.NotImplementedException();
          }
- 
          public void Scan(Document d) {
            throw new System.NotImplementedException();
          }
        }
- 
        public interface IMultiFunctionDevice: IPrinter, IScanner //
        {}
- 
        public struct MultiFunctionMachine: IMultiFunctionDevice {
          // compose this out of several modules
          private IPrinter printer;
          private IScanner scanner;
- 
          public MultiFunctionMachine(IPrinter printer, IScanner scanner) {
            if (printer == null) {
              throw new ArgumentNullException(paramName: nameof(printer));
            }
            if (scanner == null) {
              throw new ArgumentNullException(paramName: nameof(scanner));
            }
            this.printer = printer;
            this.scanner = scanner;
          }
- 
          public void Print(Document d) {
            printer.Print(d);
          }
- 
          public void Scan(Document d) {
            scanner.Scan(d);
          }
        }
- 
        public class Demo {
          static void Main(string[] args) {}
        }
      }


## Dependency Inversion Principle

High-level modules should not depend upon low-level ones; use abstractions

     using System.Collections.Generic;
    using System.Linq;
    using static System.Console;
    namespace Solid.DependencyInversion {
      // hl modules should not depend on low-level; both should depend on abstractions
      // abstractions should not depend on details; details should depend on abstractions
      public enum Relationship {
        Parent,
        Child,
        Sibling
      }

      public class Person {
        public string Name;
        // public DateTime DateOfBirth;
      }

      public interface IRelationshipBrowser {
        IEnumerable < Person > FindAllChildrenOf(string name);
      }

      public class Relationships: IRelationshipBrowser // low-level
      {
        private List < (Person, Relationship, Person) > relations = new List < (Person, Relationship, Person) > ();
        
        public void AddParentAndChild(Person parent, Person child) {
          relations.Add((parent, Relationship.Parent, child));
          relations.Add((child, Relationship.Child, parent));
        }

        public List < (Person, Relationship, Person) > Relations => relations;

        public IEnumerable < Person > FindAllChildrenOf(string name) {
          return relations
            .Where(x => x.Item1.Name == name &&
              x.Item2 == Relationship.Parent).Select(r => r.Item3);
        }
      }

      public class Research {
        public Research(Relationships relationships) {
          // high-level: find all of john's children
          //var relations = relationships.Relations;
          //foreach (var r in relations
          // .Where(x => x.Item1.Name == "John"
          // && x.Item2 == Relationship.Parent))
          //{
          // WriteLine($"John has a child called {r.Item3.Name}");
          //}
        }

        public Research(IRelationshipBrowser browser) {
          foreach(var p in browser.FindAllChildrenOf("John")) {
            WriteLine($"John has a child called {p.Name}");
          }
        }

        static void Main(string[] args) {
          var parent = new Person {
            Name = "John"
          };
          var child1 = new Person {
            Name = "Chris"
          };
          var child2 = new Person {
            Name = "Matt"
          };

          // low-level module
          var relationships = new Relationships();
          relationships.AddParentAndChild(parent, child1);
          relationships.AddParentAndChild(parent, child2);
          new Research(relationships);
        }
      }
    }