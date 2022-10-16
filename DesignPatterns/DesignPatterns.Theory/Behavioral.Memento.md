# Memento

**A token/handle representing the system state. Lets us roll back to the state when the token was generated. May or may
not directly expose state information.**

### Motivation 

- An object or system goes through changes
    - E.g., a bank account gets deposits and withdrawals
- There are different ways of navigating those changes
- One way is to record every change (Command) and teach a command to 'undo' itself
- Another is to simply save snapshots of the system.

### Summary

- Mementos are used to roll back states arbitrarily
- A memento is simply a token/handle class with (typically) no functions of its own
- A memento is not required to expose directly the state(s) to which it reverts the system
- Can be used to implement undo/redo

### Pseudocode

    // The originator holds some important data that may change over
    // time. It also defines a method for saving its state inside a
    // memento and another method for restoring the state from it.
    class Editor is
    private field text, curX, curY, selectionWidth
    method setText(text) is
    this.text = text
    method setCursor(x, y) is
    this.curX = curX
    this.curY = curY
    method setSelectionWidth(width) is
    this.selectionWidth = width
    // Saves the current state inside a memento.
    method createSnapshot():Snapshot is
    // Memento is an immutable object; that's why the
    // originator passes its state to the memento's
    // constructor parameters.
    return new Snapshot(this, text, curX, curY, selectionWidth)
    // The memento class stores the past state of the editor.
    class Snapshot is
    private field editor: Editor
    private field text, curX, curY, selectionWidth
    constructor Snapshot(editor, text, curX, curY, selectionWidth) is
    this.editor = editor
    this.text = text
    this.curX = curX
    this.curY = curY
    this.selectionWidth = selectionWidth
    // At some point, a previous state of the editor can be
    // restored using a memento object.
    method restore() is
    editor.setText(text)
    editor.setCursor(curX, curY)
    editor.setSelectionWidth(selectionWidth)
    // A command object can act as a caretaker. In that case, the
    // command gets a memento just before it changes the
    // originator's state. When undo is requested, it restores the
    // originator's state from a memento.
    class Command is
    private field backup: Snapshot
    method makeBackup() is
    backup = editor.createSnapshot()
    method undo() is
    if (backup != null)
    backup.restore()
    // ...