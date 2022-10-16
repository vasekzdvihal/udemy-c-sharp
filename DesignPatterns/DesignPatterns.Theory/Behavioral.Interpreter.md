# Interpreter

**A component that processes structured text data . Does so by turning it into separate lexical tokens (lexing) and then
interpreting sequences of said tokens (parsing).**

### Motivation 

- Textual input needs to be processed
    - E.g., turned into OOP structures
- Some examples
    - Programming language language compilers, interpreters and IDEs
    - HTML, XML and similar
    - Numeric expressions (3+4/5)
    - Regular expressions
- Turning string into OOP based structures in a complicated process

### Summary

- Barring simple cases, an interpreter acts in two stages
- Lexing turns text into a set of tokens, e.g.
  3 * (4+5) -> Lit[3] Start Lparen Lit[4] Plus Lit[6] Rparen
  - Parsing tokens into meaningful constructs


      MulticationExpression[
      Integer[3],
      AdditionExpression[
      Integer[4], Integer[5]
      ]
      ]


