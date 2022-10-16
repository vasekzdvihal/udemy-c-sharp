# Gamma categirization

- Design Patterns are typically split into three cattegories
- This is called Gamma Categorization after Erich Gamme, one of GoF authors

## Creational

- Deal with the creation (construction) of objects
- Explicit (constuctor) vs. implicit (DI,reflection, ect.)
- Wholesale (single statement) vs. piecewise (steb-by-step)

## Structural

- Concerned with the structure (e.g., class members)
- Many patterns are wrappers that mimic the underlying class' interface
- Stress the importance of good API design

## Behavioral

- They are all different; no central theme


| Jurisdiction | Creational                             | Structural                                                | Behavioral                                                                                        |
|--------------|----------------------------------------|-----------------------------------------------------------|---------------------------------------------------------------------------------------------------|
| Class        | Factory Method                         | Adapter (class), Bridge (class)                           | Template method                                                                                   |
| Object       | Abstract Factory, Prototype, Singleton | Adapter (object), Bridge (object), Flyweight, Glue, Proxy | Chain of responsibility, Command, Iterator (object), Mediator, Memento, Observer, State, Strategy | 
 | Compound     | Builder                                | Composite, Wrapper                                        | Iterator (compound), Walker                                                                       | 

