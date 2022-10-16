# Template method #

**Allows us to define the 'skeleton' of the algorithm, with concrete implementations defined in subclasses.**

### Motivation ### 

- Algorithms can be decomposed into common parts + specific
- Strategy pattern does this through composition
    - High-level algorithm uses an interface
    - Concrete implementations implement the interface
- Template Method does the same thing through inheritance
    - Overall algorithm makes use of abstract member
    - Inheritors override the abstract members
    - Parent template method invoked

### Summary ###

- Define an algorithm at a high level
- Define constituent parts as abstract methods/properties
- Inherit the algorithm class, providing necessary overrides

### Real world analogy ###
The template method approach can be used in mass housing construction. The architectural plan for building a standard
house may contain several extension points that would let a potential owner adjust some details of the resulting house.

Each building step, such as laying the foundation, framing, building walls, installing plumbing and wiring for water and
electricity, etc., can be slightly changed to make the resulting house a little bit different from others.

### Pseudocode ###

    // The abstract class defines a template method that contains a
    // skeleton of some algorithm composed of calls, usually to
    // abstract primitive operations. Concrete subclasses implement
    // these operations, but leave the template method itself
    // intact.
    class GameAI is
    // The template method defines the skeleton of an algorithm.
    method turn() is
    collectResources()
    buildStructures()
    buildUnits()
    attack()
    // Some of the steps may be implemented right in a base
    // class.
    method collectResources() is
    foreach (s in this.builtStructures) do
    s.collect()
    // And some of them may be defined as abstract.
    abstract method buildStructures()
    abstract method buildUnits()
    // A class can have several template methods.
    method attack() is
    enemy = closestEnemy()
    if (enemy == null)
    sendScouts(map.center)
    else
    sendWarriors(enemy.position)
    abstract method sendScouts(position)
    abstract method sendWarriors(position)
    // Concrete classes have to implement all abstract operations of
    // the base class but they must not override the template method
    // itself.
    class OrcsAI extends GameAI is
    method buildStructures() is
    if (there are some resources) then
    // Build farms, then barracks, then stronghold.
    method buildUnits() is
    if (there are plenty of resources) then
    if (there are no scouts)
    // Build peon, add it to scouts group.
    else
    // Build grunt, add it to warriors group.
    // ...
    method sendScouts(position) is
    if (scouts.length > 0) then
    // Send scouts to position.
    method sendWarriors(position) is
    if (warriors.length > 5) then
    // Send warriors to position.
    // Subclasses can also override some operations with a default
    // implementation.
    class MonstersAI extends GameAI is
    method collectResources() is
    // Monsters don't collect resources.
    method buildStructures() is
    // Monsters don't build structures.
    method buildUnits() is
    // Monsters don't build units.
