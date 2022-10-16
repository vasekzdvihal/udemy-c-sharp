# Adapter

### Motivation

- Electrical devices have different power (interface) requirements
    - Voltage (5V, 220V)
    - Socket/plug type (Europe, UK, USA)
- We cannot modify our gadgets to support every possible interface
    - Some support possible (e.g., 120V, 220V)
- Thus, we use a special device (an adapter) to give us the interface we require from the interface we have

### Summary

- Implementing an Adapter is easy
- Determine the API you have and the API you need
- Create a component which aggregates (has a reference to, …) the adaptee
- Intermediate representations can pile up: use caching and other optiomizations

### Real world analogy

When you travel from the US to Europe for the first time, you may get a surprise when trying to charge your laptop. The
power plug and sockets standards
are different in different countries. That’s why your US plug won’t fit a German socket. The problem can be solved by
using a power plug adapter that has
the American-style socket and the European-style plug.


### Pseudocode

    // Say you have two classes with compatible interfaces:
    // RoundHole and RoundPeg.
    class RoundHole is
    constructor RoundHole(radius) { ... }
    method getRadius() is
    // Return the radius of the hole.
    method fits(peg: RoundPeg) is
    return this.getRadius() >= peg.getRadius()
    class RoundPeg is
    constructor RoundPeg(radius) { ... }
    method getRadius() is
    // Return the radius of the peg.
    // But there's an incompatible class: SquarePeg.
    class SquarePeg is
    constructor SquarePeg(width) { ... }
    method getWidth() is
    // Return the square peg width.
    // An adapter class lets you fit square pegs into round holes.
    // It extends the RoundPeg class to let the adapter objects act
    // as round pegs.
    class SquarePegAdapter extends RoundPeg is
    // In reality, the adapter contains an instance of the
    // SquarePeg class.
    private field peg: SquarePeg
    constructor SquarePegAdapter(peg: SquarePeg) is
    this.peg = peg
    method getRadius() is
    // The adapter pretends that it's a round peg with a
    // radius that could fit the square peg that the adapter
    // actually wraps.
    return peg.getWidth() * Math.sqrt(2) / 2
    // Somewhere in client code.
    hole = new RoundHole(5)
    rpeg = new RoundPeg(5)
    hole.fits(rpeg) // true
    small_sqpeg = new SquarePeg(5)
    large_sqpeg = new SquarePeg(10)
    hole.fits(small_sqpeg) // this won't compile (incompatible types)
    small_sqpeg_adapter = new SquarePegAdapter(small_sqpeg)
    large_sqpeg_adapter = new SquarePegAdapter(large_sqpeg)
    hole.fits(small_sqpeg_adapter) // true
    hole.fits(large_sqpeg_adapter) // false