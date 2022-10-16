# Visitor #

**A pattern where a components (visitor) is allowed to traverse the entire inheritance hierarchy.
Implemented by propagating a single visit() method throughout the entire hierarchy.**

### Motivation ###

- Need to define a new operation on an entire class hierarchy
  - e.g. make a document model printable to HTML/Markdown
- Do not want to keep modifying every class in the hierarchy
- Need access to the non-common aspects of classes in the hierarchy
  - e.g. an extension method won't do
- Create an external component to handle rendering
  - But avoid type checks

### Summary ###

- Propagate an accept (Visitor v) method throughout the entire hierarchy
- Create a visitor with Visit (Foo), Visit (Bar), ... for each element in the hierarchy
- Each accept() simply calls visitor.Visit(this)
- Using dynamic, we can invoke right overload based on argument type alone (dynamic dispatch)

### Real world analogy ###

Imagine a seasoned insurance agent who's eager to get new customers. He can visit every building in a neighborhood,
trying to sell insurance to everyone he meets. Depending on the type of organization that occupies the building,
he can offer specialized insurance policies:
- If it's a residential building, he  sells medical insurance.
- If it's a bank, he sells theft insurance.
- If it's a coffee shop, he sells fire and flood insurance.

### Pseudocode ###

        // The element interface declares an `accept` method that takes
        // the base visitor interface as an argument.
        interface Shape is
        method move(x, y)
        method draw()
        method accept(v: Visitor)
        // Each concrete element class must implement the `accept`
        // method in such a way that it calls the visitor's method that
        // corresponds to the element's class.
        class Dot implements Shape is
        // ...
        // Note that we're calling `visitDot`, which matches the
        // current class name. This way we let the visitor know the
        // class of the element it works with.
        method accept(v: Visitor) is
        v.visitDot(this)
        class Circle implements Shape is
        // ...
        method accept(v: Visitor) is
        v.visitCircle(this)
        class Rectangle implements Shape is
        // ...
        method accept(v: Visitor) is
        v.visitRectangle(this)
        class CompoundShape implements Shape is
        // ...
        method accept(v: Visitor) is
        v.visitCompoundShape(this)
        // The Visitor interface declares a set of visiting methods that
        // correspond to element classes. The signature of a visiting
        // method lets the visitor identify the exact class of the
        // element that it's dealing with.
        interface Visitor is
        method visitDot(d: Dot)
        method visitCircle(c: Circle)
        method visitRectangle(r: Rectangle)
        method visitCompoundShape(cs: CompoundShape)
        // Concrete visitors implement several versions of the same
        // algorithm, which can work with all concrete element classes.
        //
        // You can experience the biggest benefit of the Visitor pattern
        // when using it with a complex object structure such as a
        // Composite tree. In this case, it might be helpful to store
        // some intermediate state of the algorithm while executing the
        // visitor's methods over various objects of the structure.
        class XMLExportVisitor implements Visitor is
        method visitDot(d: Dot) is
        // Export the dot's ID and center coordinates.
        method visitCircle(c: Circle) is
        // Export the circle's ID, center coordinates and
        // radius.
        method visitRectangle(r: Rectangle) is
        // Export the rectangle's ID, left-top coordinates,
        // width and height.
        method visitCompoundShape(cs: CompoundShape) is
        // Export the shape's ID as well as the list of its
        // children's IDs.
        // The client code can run visitor operations over any set of
        // elements without figuring out their concrete classes. The
        // accept operation directs a call to the appropriate operation
        // in the visitor object.
        class Application is
        field allShapes: array of Shapes
        method
        export () is
        exportVisitor = new XMLExportVisitor()
        foreach(shape in allShapes) do
          shape.accept(exportVisitor)