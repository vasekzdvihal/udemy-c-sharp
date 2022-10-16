# Singleton

### Motivation

- For some components it only makes sense to have one in the system
    - Database repository
    - Object factory
- E.g., the constructor call is expensive
    - We only do it once
    - We provide everyone with the same instance
- Want to prevent anyone creating additional copies
- Need to take care of lazy instantiation and thread safety

### Summary

- Making a 'safe' singleton is easy: construct a static Lazy<T> and return its Value
- Singletons are difficult to test
- Instead of directly using a singleton, consider depending on an abstraction (e.g., an interface)
- Consider defining singleton lifetime in DI container

### Real world analogy

The government is an excellent example of the Singleton pattern. A country can have only one official government.
Regardless of the personal identities of the individuals who form governments, the title, “The Government of X”, is a
global
point of access that identifies the group of people in charge.


### Pseudocode

    // The Database class defines the `getInstance` method that lets
    // clients access the same instance of a database connection
    // throughout the program.
    class Database is
    // The field for storing the singleton instance should be
    // declared static.
    private static field instance: Database
    // The singleton's constructor should always be private to
    // prevent direct construction calls with the `new`
    // operator.
    private constructor Database() is
    // Some initialization code, such as the actual
    // connection to a database server.
    // ...
    // The static method that controls access to the singleton
    // instance.
    public static method getInstance() is
    if (Database.instance == null) then
    acquireThreadLock() and then
    // Ensure that the instance hasn't yet been
    // initialized by another thread while this one
    // has been waiting for the lock's release.
    if (Database.instance == null) then
    Database.instance = new Database()
    return Database.instance
    // Finally, any singleton should define some business logic
    // which can be executed on its instance.
    public method query(sql) is
    // For instance, all database queries of an app go
    // through this method. Therefore, you can place
    // throttling or caching logic here.
    // ...
    class Application is
    method main() is
    Database foo = Database.getInstance()
    foo.query("SELECT ...")
    // ...
    Database bar = Database.getInstance()
    bar.query("SELECT ...")
    // The variable `bar` will contain the same object as
    // the variable `foo`.