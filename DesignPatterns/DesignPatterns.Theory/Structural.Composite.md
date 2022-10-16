# Composite

### Motivation

- Objects use other objects' fields/properties/members through inheritance and composition
- Composition lets us make compound objects
    - E.g., mathematical expression composed of simple expressions; or
    - A grouping of shapes that consists of several shapes
- Composite design pattern is used to threat both single (scalar) and composite objects uniformly
    - I.e., Foo and Collection<Foo> have common APIs

### Summary

- Objects can use other objects via inheritance/composition
- Some composed and singular objects need similar/identical behaviors
- Composite design pattern lets us treat both types of objects uniformly
- C# has special support for the enumeration concept
- A single object can masquerade as a collection with yield return this;

### Real world analogy

Armies of most countries are structured as hierarchies. An army consists of several divisions; a division is a set of
brigades, and a brigade consists of platoons,
which can be broken down into squads. Finally, a squad is a small group of real soldiers. Orders are given at the top of
the hierarchy and passed down onto each level until
every soldier knows what needs to be done.


### Pseudocode

    // The component interface declares common operations for both
    // simple and complex objects of a composition.
    interface Graphic is
    method move(x, y)
    method draw()
    // The leaf class represents end objects of a composition. A
    // leaf object can't have any sub-objects. Usually, it's leaf
    // objects that do the actual work, while composite objects only
    // delegate to their sub-components.
    class Dot implements Graphic is
    field x, y
    constructor Dot(x, y) { ... }
    method move(x, y) is
    this.x += x, this.y += y
    method draw() is
    // Draw a dot at X and Y.
    // All component classes can extend other components.
    class Circle extends Dot is
    field radius
    constructor Circle(x, y, radius) { ... }
    method draw() is
    // Draw a circle at X and Y with radius R.
    // The composite class represents complex components that may
    // have children. Composite objects usually delegate the actual
    // work to their children and then "sum up" the result.
    class CompoundGraphic implements Graphic is
    field children: array of Graphic
    // A composite object can add or remove other components
    // (both simple or complex) to or from its child list.
    method add(child: Graphic) is
    // Add a child to the array of children.
    method remove(child: Graphic) is
    // Remove a child from the array of children.
    method move(x, y) is
    foreach (child in children) do
    child.move(x, y)
    // A composite executes its primary logic in a particular
    // way. It traverses recursively through all its children,
    // collecting and summing up their results. Since the
    // composite's children pass these calls to their own
    // children and so forth, the whole object tree is traversed
    // as a result.
    method draw() is
    // 1. For each child component:
    // - Draw the component.
    // - Update the bounding rectangle.
    // 2. Draw a dashed rectangle using the bounding
    // coordinates.
    // The client code works with all the components via their base
    // interface. This way the client code can support simple leaf
    // components as well as complex composites.
    class ImageEditor is
    field all: CompoundGraphic
    method load() is
    all = new CompoundGraphic()
    all.add(new Dot(1, 2))
    all.add(new Circle(5, 3, 10))
    // ...
    // Combine selected components into one complex composite
    // component.
    method groupSelected(components: array of Graphic) is
    group = new CompoundGraphic()
    foreach (component in components) do
    group.add(component)
    all.remove(component)
    all.add(group)
    // All components will be drawn.
    all.draw()