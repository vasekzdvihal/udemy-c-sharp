# Command

**An object which represent an instruction to perform a particular action. Contains all the information necessary for the
action to be taken.** 

### Motivation

- Ordinary C# statements are perishable
    - Cannot undo a field/property assignment
    - Cannot directly serialize a sequence of actions (calls)
- Want an object that represent an operation
    - X should change its property Y to Z
    - Z should do W()
- Uses: GUI commands, multi-level undo/redo, macro recording and more!

### Summary

- Encapsulate all details of an operation in a separate object
- Define instruction for applying the command (either in the command itself, or elsewhere)
- Optionally define instructions for undoing the command
- Can create composite commands (a.k.a. macros)

### Real world summary

After a long walk through the city, you get to a nice restaurant and sit at the table by the window. A friendly waiter
approaches you and quickly takes your order, writing it down on a piece of paper. The waiter goes to the kitchen and
sticks the order on the wall. After a while, the order gets to the chef, who reads it and cooks the meal accordingly.
The cook places the meal on a tray along with the order. The waiter discovers the tray, checks the order to make sure
everything is as you wanted it, and brings everything to your table.

The paper order serves as a command. It remains in a queue until the chef is ready to serve it. The order contains all
the relevant information required to cook the meal. It allows the chef to start cooking right away instead of running
around clarifying the order details from you directly.


### Pseudocode

        // The base command class defines the common interface for all
        // concrete commands.
        abstract class Command is
        protected field app: Application
        protected field editor: Editor
        protected field backup: text
        constructor Command(app: Application, editor: Editor) is
        this.app = app
        this.editor = editor
        // Make a backup of the editor's state.
        method saveBackup() is
        backup = editor.text
        // Restore the editor's state.
        method undo() is
        editor.text = backup
        // The execution method is declared abstract to force all
        // concrete commands to provide their own implementations.
        // The method must return true or false depending on whether
        // the command changes the editor's state.
        abstract method execute()
        // The concrete commands go here.
        class CopyCommand extends Command is
        // The copy command isn't saved to the history since it
        // doesn't change the editor's state.
        method execute() is
        app.clipboard = editor.getSelection()
        return false
        class CutCommand extends Command is
        // The cut command does change the editor's state, therefore
        // it must be saved to the history. And it'll be saved as
        // long as the method returns true.
        method execute() is
        saveBackup()
        app.clipboard = editor.getSelection()
        editor.deleteSelection()
        return true
        class PasteCommand extends Command is
        method execute() is
        saveBackup()
        editor.replaceSelection(app.clipboard)
        return true
        // The undo operation is also a command.
        class UndoCommand extends Command is
        method execute() is
        app.undo()
        return false
        // The global command history is just a stack.
        class CommandHistory is
        private field history: array of Command
        // Last in...
        method push(c: Command) is
        // Push the command to the end of the history array.
        // ...first out
        method pop():Command is
        // Get the most recent command from the history.
        // The editor class has actual text editing operations. It plays
        // the role of a receiver: all commands end up delegating
        // execution to the editor's methods.
        class Editor is
        field text: string
        method getSelection() is
        // Return selected text.
        method deleteSelection() is
        // Delete selected text.
        method replaceSelection(text) is
        // Insert the clipboard's contents at the current
        // position.
        // The application class sets up object relations. It acts as a
        // sender: when something needs to be done, it creates a command
        // object and executes it.
        class Application is
        field clipboard: string
        field editors: array of Editors
        field activeEditor: Editor
        field history: CommandHistory
        // The code which assigns commands to UI objects may look
        // like this.
        method createUI() is
        // ...
        copy = function() { executeCommand(
        new CopyCommand(this, activeEditor)) }
        copyButton.setCommand(copy)
        shortcuts.onKeyPress("Ctrl+C", copy)
        cut = function() { executeCommand(
        new CutCommand(this, activeEditor)) }
        cutButton.setCommand(cut)
        shortcuts.onKeyPress("Ctrl+X", cut)
        paste = function() { executeCommand(
        new PasteCommand(this, activeEditor)) }
        pasteButton.setCommand(paste)
        shortcuts.onKeyPress("Ctrl+V", paste)
        undo = function() { executeCommand(
        new UndoCommand(this, activeEditor)) }
        undoButton.setCommand(undo)
        shortcuts.onKeyPress("Ctrl+Z", undo)
        // Execute a command and check whether it has to be added to
        // the history.
        method executeCommand(command) is
        if (command.execute)
        history.push(command)
        // Take the most recent command from the history and run its
        // undo method. Note that we don't know the class of that
        // command. But we don't have to, since the command knows
        // how to undo its own action.
        method undo() is
        command = history.pop()
        if (command != null)
        command.undo()