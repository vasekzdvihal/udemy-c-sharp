# Prototype

## Motivation

- Complicated objects (e.g., cars) aren't designed from scratch
    - They reiterate existing designs
- An existing (partially or fully constructed) design is a Prototype
- We make a copy (clone) the prototype and customize it
    - Requires 'deep copy' support
- We make the cloning convenient (e.g., via a Factory)

## Summary

- To implement a prototype, partially construct an object and store it somewhere
- Clone the prototype
    - Implement you own deep copy functionality; or
    - Serialize and deserialize
- Customize the resulting instance

## Real world analogy

In real life, prototypes are used for performing various tests before starting mass production of a product. However,
in this case, prototypes don’t participate in any actual production, playing a passive role instead.

Since industrial prototypes don’t really copy themselves, a much closer analogy to the pattern is the process of mitotic
cel
division (biology, remember?). After mitotic division, a pair of identical cells is formed. The original cell acts as a
prototype and takes an active role in creating the copy.

## Pseudocode

    // Base prototype.
    abstract class Shape is
    field X: int
    field Y: int
    field color: string
    // A regular constructor.
    constructor Shape() is
    // ...
    // The prototype constructor. A fresh object is initialized
    // with values from the existing object.
    constructor Shape(source: Shape) is
    this()
    this.X = source.X
    this.Y = source.Y
    this.color = source.color
    // The clone operation returns one of the Shape subclasses.
    abstract method clone():Shape
    // Concrete prototype. The cloning method creates a new object
    // and passes it to the constructor. Until the constructor is
    // finished, it has a reference to a fresh clone. Therefore,
    // nobody has access to a partly-built clone. This keeps the
    // cloning result consistent.
    class Rectangle extends Shape is
    field width: int
    field height: int
    constructor Rectangle(source: Rectangle) is
    // A parent constructor call is needed to copy private
    // fields defined in the parent class.
    super(source)
    this.width = source.width
    this.height = source.height
    method clone():Shape is
    return new Rectangle(this)
    class Circle extends Shape is
    field radius: int
    constructor Circle(source: Circle) is
    super(source)
    this.radius = source.radius
    method clone():Shape is
    return new Circle(this)
    // Somewhere in the client code.
    class Application is
    field shapes: array of Shape
    constructor Application() is
    Circle circle = new Circle()
    circle.X = 10
    circle.Y = 10
    circle.radius = 20
    shapes.add(circle)
    Circle anotherCircle = circle.clone()
    shapes.add(anotherCircle)
    // The `anotherCircle` variable contains an exact copy
    // of the `circle` object.
    Rectangle rectangle = new Rectangle()
    rectangle.width = 10
    rectangle.height = 20
    shapes.add(rectangle)
    method businessLogic() is
    // Prototype rocks because it lets you produce a copy of
    // an object without knowing anything about its type.
    Array shapesCopy = new Array of Shapes.
    // For instance, we don't know the exact elements in the
    // shapes array. All we know is that they are all
    // shapes. But thanks to polymorphism, when we call the
    // `clone` method on a shape the program checks its real
    // class and runs the appropriate clone method defined
    // in that class. That's why we get proper clones
    // instead of a set of simple Shape objects.
    foreach (s in shapes) do
    shapesCopy.add(s.clone())
    // The `shapesCopy` array contains exact copies of the
    // `shape` array's children.