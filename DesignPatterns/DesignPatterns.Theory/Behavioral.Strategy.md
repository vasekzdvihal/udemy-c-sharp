# Strategy #

**Enables the exact behavior of a system to be selected either at run-time (dynamic) or compile-time (static)**

### Motivation ###

- Many algorithms can be decomposed into higher and lower level parts
- Making tea can be decomposed into
    - The process of making a hot beverage (boil water, pour into cup);and
    - Tea-specific things (put teabag into water)
- The high level algorithm can then be reused for making coffee or hot chocolate
    - Supported by beverage-specific strategies

### Summary ###

- Define an algorithm at a high level
- Define the interface you expect each strategy to follow
- Provide for either dynamic or static composition of strategy in the overall algorithm

### Real world analogy ###
Imagine that you have to get to the airport. You can catch a bus, order a cab, or get on your bicycle. These are your
transportation strategies. You can pick one of the strategies depending on factors such as budget or time constraints.

### Pseudocode ###

        // The strategy interface declares operations common to all
        // supported versions of some algorithm. The context uses this
        // interface to call the algorithm defined by the concrete
        // strategies.
        interface Strategy is
        method execute(a, b)
        // Concrete strategies implement the algorithm while following
        // the base strategy interface. The interface makes them
        // interchangeable in the context.
        class ConcreteStrategyAdd implements Strategy is
        method execute(a, b) is
        return a + b
        class ConcreteStrategySubtract implements Strategy is
        method execute(a, b) is
        return a - b
        class ConcreteStrategyMultiply implements Strategy is
        method execute(a, b) is
        return a * b
        // The context defines the interface of interest to clients.
        class Context is
        // The context maintains a reference to one of the strategy
        // objects. The context doesn't know the concrete class of a
        // strategy. It should work with all strategies via the
        // strategy interface.
        private strategy: Strategy
        // Usually the context accepts a strategy through the
        // constructor, and also provides a setter so that the
        // strategy can be switched at runtime.
        method setStrategy(Strategy strategy) is
        this.strategy = strategy
        // The context delegates some work to the strategy object
        // instead of implementing multiple versions of the
        // algorithm on its own.
        method executeStrategy(int a, int b) is
        return strategy.execute(a, b)
        // The client code picks a concrete strategy and passes it to
        // the context. The client should be aware of the differences
        // between strategies in order to make the right choice.
        class ExampleApplication is
        method main() is
        Create context object.
        Read first number.
        Read last number.
        Read the desired action from user input.
        if (action == addition) then
        context.setStrategy(new ConcreteStrategyAdd())
        if (action == subtraction) then
        context.setStrategy(new ConcreteStrategySubtract())
        if (action == multiplication) then
        context.setStrategy(new ConcreteStrategyMultiply())
        result = context.executeStrategy(First number, Second number)
        Print result.