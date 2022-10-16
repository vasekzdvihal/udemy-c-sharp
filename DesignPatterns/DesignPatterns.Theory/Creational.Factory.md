# Factory

### Motivation

- Object creation logic becomes too convoluted
- Constructor is not descriptive
    - Name mandated by name of containing type
    - Cannot overload with same sets of arguments with different names
    - Can turn into 'optional parameter hell'
- Object creation (non-piecewise, unlike Builder) can be outsourced to
    - A separate function (Factory Method)
    - That may exist in a separate class (Factory)
    - Can create hierarchy of factories with Abstract Factory


### Summary

- A factory method is a static method that creates objects
- A factory can take care of object creation
- A factory can be external or reside inside the object as an inner class
- Hierarchies of factories can be used to create related objects


### Pseudocode 

    // The creator class declares the factory method that must
    // return an object of a product class. The creator's subclasses
    // usually provide the implementation of this method.
    class Dialog is
    // The creator may also provide some default implementation
    // of the factory method.
    abstract method createButton():Button
    // Note that, despite its name, the creator's primary
    // responsibility isn't creating products. It usually
    // contains some core business logic that relies on product
    // objects returned by the factory method. Subclasses can
    // indirectly change that business logic by overriding the
    // factory method and returning a different type of product
    // from it.
    method render() is
    // Call the factory method to create a product object.
    Button okButton = createButton()
    // Now use the product.
    okButton.onClick(closeDialog)
    okButton.render()
    // Concrete creators override the factory method to change the
    // resulting product's type.
    class WindowsDialog extends Dialog is
    method createButton():Button is
    return new WindowsButton()
    class WebDialog extends Dialog is
    method createButton():Button is
    return new HTMLButton()
    // The product interface declares the operations that all
    // concrete products must implement.
    interface Button is
    method render()
    method onClick(f)
    // Concrete products provide various implementations of the
    // product interface.
    class WindowsButton implements Button is
    method render(a, b) is
    // Render a button in Windows style.
    method onClick(f) is
    // Bind a native OS click event.
    class HTMLButton implements Button is
    method render(a, b) is
    // Return an HTML representation of a button.
    method onClick(f) is
    // Bind a web browser click event.
    class Application is
    field dialog: Dialog
    // The application picks a creator's type depending on the
    // current configuration or environment settings.
    method initialize() is
    config = readApplicationConfigFile()
    if (config.OS == "Windows") then
    dialog = new WindowsDialog()
    else if (config.OS == "Web") then
    dialog = new WebDialog()
    else
    throw new Exception("Error! Unknown operating system.")
    // The client code works with an instance of a concrete
    // creator, albeit through its base interface. As long as
    // the client keeps working with the creator via the base
    // interface, you can pass it any creator's subclass.
    method main() is
    this.initialize()
    dialog.render()
