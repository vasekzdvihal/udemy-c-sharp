# Bridge

**A mechanism that decouples an interface (hierarchy) from an implementation (hierarchy).**

### Motivation

- Bridge prevents a 'Cartesian product' complexity explosion
- Example:
    - Base class ThreadScheduler
    - Can be preemtive or cooperative
    - Can run on Window or Unix
    - End up with a 2x2 scenario: WindowsPTS, UnixPTS, WindowsCTS, UnixCTS
- Bridge pattern avoids the entity explosion

### Summary

- Decouple abstraction from implementation
- Both can exist as hierarchies
- A stronger form of encapsulation

### Pseudocode


    // The "abstraction" defines the interface for the "control"
    // part of the two class hierarchies. It maintains a reference
    // to an object of the "implementation" hierarchy and delegates
    // all of the real work to this object.
    class RemoteControl is
    protected field device: Device
    constructor RemoteControl(device: Device) is
    this.device = device
    method togglePower() is
    if (device.isEnabled()) then
    device.disable()
    else
    device.enable()
    method volumeDown() is
    device.setVolume(device.getVolume() - 10)
    method volumeUp() is
    device.setVolume(device.getVolume() + 10)
    method channelDown() is
    device.setChannel(device.getChannel() - 1)
    method channelUp() is
    device.setChannel(device.getChannel() + 1)
    // You can extend classes from the abstraction hierarchy
    // independently from device classes.
    class AdvancedRemoteControl extends RemoteControl is
    method mute() is
    device.setVolume(0)
    // The "implementation" interface declares methods common to all
    // concrete implementation classes. It doesn't have to match the
    // abstraction's interface. In fact, the two interfaces can be
    // entirely different. Typically the implementation interface
    // provides only primitive operations, while the abstraction
    // defines higher-level operations based on those primitives.
    interface Device is
    method isEnabled()
    method enable()
    method disable()
    method getVolume()
    method setVolume(percent)
    method getChannel()
    method setChannel(channel)
    // All devices follow the same interface.
    class Tv implements Device is
    // ...
    class Radio implements Device is
    // ...
    // Somewhere in client code.
    tv = new Tv()
    remote = new RemoteControl(tv)
    remote.togglePower()
    radio = new Radio()
    remote = new AdvancedRemoteControl(radio)