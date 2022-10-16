# Observer

**The observer is an object that wishes to be informed about events happening in the system. The entity generating the
events is an observable.**

### Motivation

- We need to be informed when certain things happen
    - Object's property changes
    - Object does something
    - Some external event occurs
- We want to listen to event and notified when they occur
- Built into C# with the event keyword
    - But then what is this IObservable<T>/IObserver<T> for?
    - What about INotifyPropertyChanging/Changed?
    - And what are BindingList<T>/ObservableCollection<T>?

### Summary

- Observer is an intrusive approach: an observable must provide an event to subscribe to
- Special care must be taken to prevent issues in multithreaded scenarios
- .NET comes with observable collections
- IObserver<T>/IObservable<T> are used in stream processing (Reactive Extensions)

### Real world analogy

If you subscribe to a newspaper or magazine, you no longer need to go to the store to check if the next issue is
available. Instead, the publisher sends new issues directly to your mailbox right after publication or even in advance.

The publisher maintains a list of subscribers and knows which magazines they’re interested in. Subscribers can leave the
list at any time when they wish to stop the publisher sending new magazine issues to them.

### Pseudocode

    // The base publisher class includes subscription management
    // code and notification methods.
    class EventManager is
    private field listeners: hash map of event types and listeners
    method subscribe(eventType, listener) is
    listeners.add(eventType, listener)
    method unsubscribe(eventType, listener) is
    listeners.remove(eventType, listener)
    method notify(eventType, data) is
    foreach (listener in listeners.of(eventType)) do
    listener.update(data)
    // The concrete publisher contains real business logic that's
    // interesting for some subscribers. We could derive this class
    // from the base publisher, but that isn't always possible in
    // real life because the concrete publisher might already be a
    // subclass. In this case, you can patch the subscription logic
    // in with composition, as we did here.
    class Editor is
    public field events: EventManager
    private field file: File
    constructor Editor() is
    events = new EventManager()
    // Methods of business logic can notify subscribers about
    // changes.
    method openFile(path) is
    this.file = new File(path)
    events.notify("open", file.name)
    method saveFile() is
    file.write()
    events.notify("save", file.name)
    // ...
    // Here's the subscriber interface. If your programming language
    // supports functional types, you can replace the whole
    // subscriber hierarchy with a set of functions.
    interface EventListener is
    method update(filename)
    // Concrete subscribers react to updates issued by the publisher
    // they are attached to.
    class LoggingListener implements EventListener is
    private field log: File
    private field message
    constructor LoggingListener(log_filename, message) is
    this.log = new File(log_filename)
    this.message = message
    method update(filename) is
    log.write(replace('%s',filename,message))
    class EmailAlertsListener implements EventListener is
    private field email: string
    constructor EmailAlertsListener(email, message) is
    this.email = email
    this.message = message
    method update(filename) is
    system.email(email, replace('%s',filename,message))
    // An application can configure publishers and subscribers at
    // runtime.
    class Application is
    method config() is
    editor = new Editor()
    logger = new LoggingListener(
    "/path/to/log.txt",
    "Someone has opened the file: %s")
    editor.events.subscribe("open", logger)
    emailAlerts = new EmailAlertsListener(
    "admin@example.com",
    "Someone has changed the file: %s")
    editor.events.subscribe("save", emailAlerts)