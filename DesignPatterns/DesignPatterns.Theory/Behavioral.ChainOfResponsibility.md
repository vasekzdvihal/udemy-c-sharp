# Chain of Responsibility

**A chain of components who all get a chance to process a command or a query, optionally having default processing
implementation and an ability to terminate the processing chain**

### Motivation

- Unethical behavior by an employee; who takes the blame?
    - Employee
    - Manager
    - CEO
- You click a graphical element on a form
    - Button handles it, stop further processing
    - Underlying group box
    - Underlying window
- CCG computer game
    - Creature has attack and defense values
    - Those can be boosted by other cards

### Summary

- Chain of Responsibility can be implemented as a chain of references or a centralized construct
- Enlist objects in the chain, possibly controlling their order
- Object removal from chain (e.g., in Dispose())

### Real world analogy

You’ve just bought and installed a new piece of hardware on your computer. Since you’re a geek, the computer has several
operating systems installed. You try to boot all of them to see whether the hardware is supported. Windows detects and
enables the hardware automatically. However, your beloved Linux refuses to work with the new hardware. With a small
flicker of hope, you decide to call the tech-support phone number written on the box.

The first thing you hear is the robotic voice of the autoresponder. It suggests nine popular solutions to various
problems, none of which are relevant to your case. After a while, the robot connects you to a live operator.

Alas, the operator isn’t able to suggest anything specific either. He keeps quoting lengthy excerpts from the manual,
refusing to listen to your comments. After hearing the phrase “have you tried turning the computer off and on again?”
for the 10th time, you demand to be connected to a proper engineer.

Eventually, the operator passes your call to one of the engineers, who had probably longed for a live human chat for
hours as he sat in his lonely server room in the dark basement of some office building. The engineer tells you where to
download proper drivers for your new hardware and how to install them on Linux. Finally, the solution! You end the call,
bursting with joy.

### Pseudocode

        // The handler interface declares a method for building a chain
        // of handlers. It also declares a method for executing a
        // request.
        interface ComponentWithContextualHelp is
        method showHelp()
        // The base class for simple components.
        abstract class Component implements ComponentWithContextualHelp is
        field tooltipText: string
        // The component's container acts as the next link in the
        // chain of handlers.
        protected field container: Container
        // The component shows a tooltip if there's help text
        // assigned to it. Otherwise it forwards the call to the
        // container, if it exists.
        method showHelp() is
        if (tooltipText != null)
        // Show tooltip.
        else
        container.showHelp()
        // Containers can contain both simple components and other
        // containers as children. The chain relationships are
        // established here. The class inherits showHelp behavior from
        // its parent.
        abstract class Container extends Component is
        protected field children: array of Component
        method add(child) is
        children.add(child)
        child.container = this
        // Primitive components may be fine with default help
        // implementation...
        class Button extends Component is
        // ...
        // But complex components may override the default
        // implementation. If the help text can't be provided in a new
        // way, the component can always call the base implementation
        // (see Component class).
        class Panel extends Container is
        field modalHelpText: string
        method showHelp() is
        if (modalHelpText != null)
        // Show a modal window with the help text.
        else
        super.showHelp()
        // ...same as above...
        class Dialog extends Container is
        field wikiPageURL: string
        method showHelp() is
        if (wikiPageURL != null)
        // Open the wiki help page.
        else
        super.showHelp()
        // Client code.
        class Application is
        // Every application configures the chain differently.
        method createUI() is
        dialog = new Dialog("Budget Reports")
        dialog.wikiPageURL = "http://..."
        panel = new Panel(0, 0, 400, 800)
        panel.modalHelpText = "This panel does..."
        ok = new Button(250, 760, 50, 20, "OK")
        ok.tooltipText = "This is an OK button that..."
        cancel = new Button(320, 760, 50, 20, "Cancel")
        // ...
        panel.add(ok)
        panel.add(cancel)
        dialog.add(panel)
        // Imagine what happens here.
        method onF1KeyPress() is
        component = this.getComponentAtMouseCoords()
        component.showHelp()