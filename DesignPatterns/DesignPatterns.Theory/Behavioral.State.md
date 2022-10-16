# State #

**A pattern in which the object's behavior is determined by its state. An object transitions from one state to another (
something needs to trigger a transition).
A formalized construct which manages state and transitions is called a sate machines.**

### Motivation ###

- Consider an ordinary telephone
- What you do wit it depends on the state of the phone/line
    - If it's ringing or you want to make a call, you can pick it up
    - Phone must be off the hook to talk/make a call
    - If you try calling someone, and it's busy, you put the handset down
- Changes in state can be explicit or in response to event (Observer pattern)

### Summary ###

- Given sufficient complexity it pays to formally define possible states and events/triggers
- Can define
  - State entry/exit behaviors
  - Action when a particular event causes a transition
  - Guard conditions enabling/disabling a transitions
  - Default action when no transitions are found for an event

### Real world analogy ###

The buttons and switches in your smartphone behave differently depending on the current state of the device:

- When the phone is unlocked, pressing buttons leads to executing various functions.
- When the phone is locked, pressing any button leads to the unlock screen.
- When the phone’s charge is low, pressing any button shows the charging screen.

### Pseudocode

    // The AudioPlayer class acts as a context. It also maintains a
    // reference to an instance of one of the state classes that
    // represents the current state of the audio player.
    class AudioPlayer is
    field state: State
    field UI, volume, playlist, currentSong
    constructor AudioPlayer() is
    this.state = new ReadyState(this)
    // Context delegates handling user input to a state
    // object. Naturally, the outcome depends on what state
    // is currently active, since each state can handle the
    // input differently.
    UI = new UserInterface()
    UI.lockButton.onClick(this.clickLock)
    UI.playButton.onClick(this.clickPlay)
    UI.nextButton.onClick(this.clickNext)
    UI.prevButton.onClick(this.clickPrevious)
    // Other objects must be able to switch the audio player's
    // active state.
    method changeState(state: State) is
    this.state = state
    // UI methods delegate execution to the active state.
    method clickLock() is
    state.clickLock()
    method clickPlay() is
    state.clickPlay()
    method clickNext() is
    state.clickNext()
    method clickPrevious() is
    state.clickPrevious()
    // A state may call some service methods on the context.
    method startPlayback() is
    // ...
    method stopPlayback() is
    // ...
    method nextSong() is
    // ...
    method previousSong() is
    // ...
    method fastForward(time) is
    // ...
    method rewind(time) is
    // ...
    // The base state class declares methods that all concrete
    // states should implement and also provides a backreference to
    // the context object associated with the state. States can use
    // the backreference to transition the context to another state.
    abstract class State is
    protected field player: AudioPlayer
    // Context passes itself through the state constructor. This
    // may help a state fetch some useful context data if it's
    // needed.
    constructor State(player) is
    this.player = player
    abstract method clickLock()
    abstract method clickPlay()
    abstract method clickNext()
    abstract method clickPrevious()
    // Concrete states implement various behaviors associated with a
    // state of the context.
    class LockedState extends State is
    // When you unlock a locked player, it may assume one of two
    // states.
    method clickLock() is
    if (player.playing)
    player.changeState(new PlayingState(player))
    else
    player.changeState(new ReadyState(player))
    method clickPlay() is
    // Locked, so do nothing.
    method clickNext() is
    // Locked, so do nothing.
    method clickPrevious() is
    // Locked, so do nothing.
    // They can also trigger state transitions in the context.
    class ReadyState extends State is
    method clickLock() is
    player.changeState(new LockedState(player))
    method clickPlay() is
    player.startPlayback()
    player.changeState(new PlayingState(player))
    method clickNext() is
    player.nextSong()
    method clickPrevious() is
    player.previousSong()
    class PlayingState extends State is
    method clickLock() is
    player.changeState(new LockedState(player))
    method clickPlay() is
    player.stopPlayback()
    player.changeState(new ReadyState(player))
    method clickNext() is
    if (event.doubleclick)
    player.nextSong()
    else
    player.fastForward(5)
    method clickPrevious() is
    if (event.doubleclick)
    player.previous()
    else
    player.rewind(5)