# Garbage Collection

## Garbage collection basics

- Memory management is a major concern for developers
- Unmanaged programming languages often exhibit one serious behaviour
    - Memory leaks
    - Allocated memory that is never reclaimed
- The .NET Framework provides a functionality called Garbage Collection (GC) to avoid the vast majority of memory leaks
    - Unused unreferenced objects are automatically removed from memory
    - GC works only on managed memory objects
- There are a few details about GC that you should know
    - GC does not happen immediately once an object is orphaned
    - GC occurs periodically based on available memory, current memory allocation and time since the last collection
    - The delay between collections can range from seconds to days
    - Not all orphaned objects are reclaimed during every collection
    - There are a few instances when helping the GC along is necessary
    - You can force Garbage Collection to occur by calling GC.Collect();

## Understand Dispose

- Some resources require explicit code to release resources
- Open files, databases connections, locks, OS handles and unmanaged objects
- Disposal provides the teardown code to release resources
- The GC reclaims the memory at a later date
- Disposal is supported via the 'IDisposable' interface
    - Includes a single method
    - void Dispose();
- There are two ways to implement Disposal
    - Calling the Dispose() method
    - The 'using' statement
- The compiler converts this to a try/finally construct that includes Dispose() in the finally block

- A Finalizer is the opposite of a constructor
    - A constructor is the first opportunity to work with an object
    - A finalizer is the last opportunity to work with an object
    - A finalizer is method whose name begins with a ~
- The best way to understand a finalizer is to understand how Garbage Collection works...

- Objects utilise a pointer on the stack that references to the data stored on the heap
- When the GC runs, it sees that both objects still have references so no action is taken
- The next time the GC runs, both Address:1 does not have a Finalizer, it is removed
- Since Address:2 has a Finelizer, it is placed into a queue and kept alive
- The finelizer thread runs and identifies objects in the queue and runs their finalization methods
- Once finalization has been run, the object becomes orphaned
- The next time GC occurs, the object will be deleted
- Finalizers should only be used when necessary
    - Slow the allocation and collection of memory
    - Prolong the life of objects
    - Impossible to predict the order in which finalizers for a set of objects will be called
    - If code in a finalizer blocks, references other finalizable objects, or throws exceptions finalization will not
      occur

## Understanding IDisposable 

- Applications function by creating objects
    - Create and maintain connections to resources
    - Require memory
- Objects should be 'cleaned up' when their use is no longer required
- Objects fall into two categories in a .NET application
    - MANAGED
        - Managed objects are automatically cleaned up by the GC functionality
    - UNMANAGED
        - Non .NET objects are not cleaned up by GC and must be explicitly closed/remained
        - This process is called Disposal
    - The .NET Framework defines on interface for types requiring explicit disposal action
        - IDisposable
        - Defines a single methods void Dispose()

## Implementing IDisposable

``` csharp
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.Runtime.InteropServices;
    
    class BaseClass : IDisposable
    {
    // Flag: Has Dispose already been called?
    bool disposed = false;
    // Instantiate a SafeHandle instance.
    SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
    
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
    
            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }
    
            disposed = true;
        }
    }
```

- IDisposable should only be implemented when unmanaged resources are being used
- Implementing IDisposable can introduce inefficiencies due to the overhead of the finalization process
- Microsoft suggest using a pattern when implementing IDisposable
- The Dispose Pattern is intended to standardise the usage and implementation of finalizers and the IDisposable
  interface

- Lets take a look at the pattern
    - Create a variable that will be used to keep track of whether Dispose has been called [8]
        - disposed will be false by default
    - Dispose() is the implementation of the IDiposable interface [13]
        - Make a call to an overloaded Dispose method passing 'true' to the required boolean parameter [15]
    - If disposed is 'true' , then Disposal has occurred
        - Exit the method and return to the caller [22]
    - Passing 'true' as an argument indicates that the method is being invoked by the user to carry out disposal
        - Release managed and unmanaged objects
        - If the method is invoked from the Finalizer, 'false' will be passed [25]
    - If Disposal was successful, set disposed to 'true' [32]
    - Execution is returned the calling method
    - GC.SupressFinalize(this) is called to the prevent the Finalizer from being called since Disposal action has been
      taken [16]
    - The ~DisposableClass() method is the Finalizer
        - It will only be called by the GC
        - When GC calls the Finalizer, 'false' is is passed to Dispose because the FC is in the process of cleaning up
          managed objects and Disposal is not needed