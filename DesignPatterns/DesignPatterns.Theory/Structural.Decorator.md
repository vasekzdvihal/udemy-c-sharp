# Decorator

**Facilitates the addition of behaviors to individual objects without inheriting from them.**

### Motivation

- Want to augment an object with additional functionality
- Do not want to rewrite or alter existing code (OCP)
- Want to keep new functionality separate (SRP)
- Need to be able to interact with existing structures
- Two options:
    - Inherit from required object if possible; some objects are sealed
    - Build a Decorator, which simply references to decorated object(s)


### Summary

- A decorator keeps the reference to the decorated object(s)
- May or may not proxy over calls
    - Use R# Generate Delegated Members
- Exist in a static variation
    - X<Y<Foo>>
    - Very limited due to inability to inherit from type parameters

### Real world analogy

Wearing clothes is an example of using decorators. When you’re cold, you wrap yourself in a sweater.
If you’re still cold with a sweater, you can wear a jacket on top. If it’s raining, you can put on a raincoat. All of
these garments
“extend” your basic behavior but aren’t part of you, and you can easily take off any piece of clothing whenever you
don’t need it.

### Pseudocode 

    // The component interface defines operations that can be
    // altered by decorators.
    interface DataSource is
    method writeData(data)
    method readData():data
    // Concrete components provide default implementations for the
    // operations. There might be several variations of these
    // classes in a program.
    class FileDataSource implements DataSource is
    constructor FileDataSource(filename) { ... }
    method writeData(data) is
    // Write data to file.
    method readData():data is
    // Read data from file.
    // The base decorator class follows the same interface as the
    // other components. The primary purpose of this class is to
    // define the wrapping interface for all concrete decorators.
    // The default implementation of the wrapping code might include
    // a field for storing a wrapped component and the means to
    // initialize it.
    class DataSourceDecorator implements DataSource is
    protected field wrappee: DataSource
    constructor DataSourceDecorator(source: DataSource) is
    wrappee = source
    // The base decorator simply delegates all work to the
    // wrapped component. Extra behaviors can be added in
    // concrete decorators.
    method writeData(data) is
    wrappee.writeData(data)
    // Concrete decorators may call the parent implementation of
    // the operation instead of calling the wrapped object
    // directly. This approach simplifies extension of decorator
    // classes.
    method readData():data is
    return wrappee.readData()
    // Concrete decorators must call methods on the wrapped object,
    // but may add something of their own to the result. Decorators
    // can execute the added behavior either before or after the
    // call to a wrapped object.
    class EncryptionDecorator extends DataSourceDecorator is
    method writeData(data) is
    // 1. Encrypt passed data.
    // 2. Pass encrypted data to the wrappee's writeData
    // method.
    method readData():data is
    // 1. Get data from the wrappee's readData method.
    // 2. Try to decrypt it if it's encrypted.
    // 3. Return the result.
    // You can wrap objects in several layers of decorators.
    class CompressionDecorator extends DataSourceDecorator is
    method writeData(data) is
    // 1. Compress passed data.
    // 2. Pass compressed data to the wrappee's writeData
    // method.
    method readData():data is
    // 1. Get data from the wrappee's readData method.
    // 2. Try to decompress it if it's compressed.
    // 3. Return the result.
    // Option 1. A simple example of a decorator assembly.
    class Application is
    method dumbUsageExample() is
    source = new FileDataSource("somefile.dat")
    source.writeData(salaryRecords)
    // The target file has been written with plain data.
    source = new CompressionDecorator(source)
    source.writeData(salaryRecords)
    // The target file has been written with compressed
    // data.
    source = new EncryptionDecorator(source)
    // The source variable now contains this:
    // Encryption > Compression > FileDataSource
    source.writeData(salaryRecords)
    // The file has been written with compressed and
    // encrypted data.
    // Option 2. Client code that uses an external data source.
    // SalaryManager objects neither know nor care about data
    // storage specifics. They work with a pre-configured data
    // source received from the app configurator.
    class SalaryManager is
    field source: DataSource
    constructor SalaryManager(source: DataSource) { ... }
    method load() is
    return source.readData()
    method save() is
    source.writeData(salaryRecords)
    // ...Other useful methods...
    // The app can assemble different stacks of decorators at
    // runtime, depending on the configuration or environment.
    class ApplicationConfigurator is
    method configurationExample() is
    source = new FileDataSource("salary.dat")
    if (enabledEncryption)
    source = new EncryptionDecorator(source)
    if (enabledCompression)
    source = new CompressionDecorator(source)
    logger = new SalaryManager(source)
    salary = logger.load()
    // ...