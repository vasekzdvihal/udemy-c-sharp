# Null object

**A no-op object that conforms to the required interface satisfying a dependency requirement of some other object.**

### Motivation

- When component A uses component B, it typically assumes that B is non-null
    - You inject B, not B? or some Option<B>
    - You do not check for null (?.) on every call
- There is no option of telling A not to use an instance of B
    - Its use is hard-coded
- Thus, we build a no-op, non-functioning inheritor of B and pass it into A

### Summary

- Implement the required interface
- Rewrite the methods with empty bodies
    - If method is non-void, return default (T)
    - If these values are ever used, you are in trouble
- Supply an instance of Null Object in place of actual object
- Dynamic construction possible
    - With associated performance implications
