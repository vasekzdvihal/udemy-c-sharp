# Flyweight

**A space optimization technique that lets us use less memory by storing externally the data associated with similar
objects.**

### Motivation

- Avoid redundancy when storing data
- E.g., MMORPG
    - Plenty of users identical first/last names
    - No sense in storing same first/last name over and over again
    - Store a list of names and pointers to them
- .NET performs string interning, so an identical string is stored only once
- E.g., bold or italic text in the console
    - Don*t want each character to have a formatting character
    - Operate on ranges (e.g., line number, start/end positions)

### Summary

- Store common data externally
- Define the idea of 'ranges' on homogeneous collections and store data related to those ranges
- .NET string interning is the Flyweight pattern

### Pseudocode 

    // The flyweight class contains a portion of the state of a
    // tree. These fields store values that are unique for each
    // particular tree. For instance, you won't find here the tree
    // coordinates. But the texture and colors shared between many
    // trees are here. Since this data is usually BIG, you'd waste a
    // lot of memory by keeping it in each tree object. Instead, we
    // can extract texture, color and other repeating data into a
    // separate object which lots of individual tree objects can
    // reference.
    class TreeType is
    field name
    field color
    field texture
    constructor TreeType(name, color, texture) { ... }
    method draw(canvas, x, y) is
    // 1. Create a bitmap of a given type, color & texture.
    // 2. Draw the bitmap on the canvas at X and Y coords.
    // Flyweight factory decides whether to re-use existing
    // flyweight or to create a new object.
    class TreeFactory is
    static field treeTypes: collection of tree types
    static method getTreeType(name, color, texture) is
    type = treeTypes.find(name, color, texture)
    if (type == null)
    type = new TreeType(name, color, texture)
    treeTypes.add(type)
    return type
    // The contextual object contains the extrinsic part of the tree
    // state. An application can create billions of these since they
    // are pretty small: just two integer coordinates and one
    // reference field.
    class Tree is
    field x,y
    field type: TreeType
    constructor Tree(x, y, type) { ... }
    method draw(canvas) is
    type.draw(canvas, this.x, this.y)
    // The Tree and the Forest classes are the flyweight's clients.
    // You can merge them if you don't plan to develop the Tree
    // class any further.
    class Forest is
    field trees: collection of Trees
    method plantTree(x, y, name, color, texture) is
    type = TreeFactory.getTreeType(name, color, texture)
    tree = new Tree(x, y, type)
    trees.add(tree)
    method draw(canvas) is
    foreach (tree in trees) do
    tree.draw(canvas)