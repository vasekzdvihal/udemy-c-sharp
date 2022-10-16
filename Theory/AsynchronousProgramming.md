# Asynchronous Programming

## Understanding Multithreading

- Computers accomplish concurrency via a functionality called multithreading
- A 'thread' is an execution path
    - Runs within separate operating system process
    - Operates independently of other execution path
- Most programs start in a single-thread environment
    - The single thread is created automatically by the operating system
    - Also referred to as the 'main' thread
- The Thread class can be used to create and start new thread
    - One thread runs in the process's isolated environment
    - One action must wait for another action to complete
- Multi-threaded programs utilise multiple threads
    - Share a single process's execution environment
    - On a single-core CPU processing switches back and forth from thread to thread
    - On a multi-core CPU processing the two threads can execute in parallel

## Creating a Thread

- Another thread can be created and started
    - Using the Thread class
    - Calling the Start method


    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    namespace Multithreading_Example {
      class Program {
        static void Main(string[] args) {
          Thread t = new Thread(WriteX);
          t.Start();

          for (int i = 0; i < 1000; i++) {
            Console.Write("O");
          }
        }

        static void WriteX() {
          for (int i = 0; i < 1000; i++) {
            Console.Write(".");
          }
        }
      }
    }

## Managing a Thread

- Once a new thread is started, there are a number of properties and methods that can be called to manage it
  - The IsAlive property returns true until the thread ends
  - Each thread has a Name property useful for debugging
  - Thread.CurrentThread gives you the currently executing thread
  - A running thread can be paused using Thread.Sleep
  - You can wait for another thread to end by calling it's Join method
  - You check to see if a thread is blocked by checking it's ThreadState property
  - A thread's Priority property determines how much execution time it get's relative to other threads
- There two main types of threads
  - FOREGROUND THREADS
    - Keep the application alive for as long as they are running
    - When completed, the application ends and any background threads terminate immediately
    - By default, threads that are explicitly created are foreground threads
  - BACKGROUND THREADS
    - Threads that are terminated automatically when all foreground threads are closed or completed
    - Created by setting the IsBackground property to true
    - IsBackground can be used to change a thread's status
    - Excellent for tasks like polling services or logging into

## Thread Pool

- Starting a new thread requires resources

  - Normally a few hundred microseconds
  - This can become noticeable if very many threads are being created for short operations
- The ThreadPool avoids this overhead by maintaining a pool of pre-created, recyclable threads
- There are a few things to consider about the ThreadPool
  - You cannot set the Name of pooled thread, making debugging more difficult
  - Pooled threads are always background threads
  - Blocking pooled threads can degrade performance
  - A pooled thread's priority can be changed it will be restored to normal when released back to pool
- The Task.Run method is new in the .Net Framework 4.5
  - It starts a task that is executed on a thread from the ThreadPool
  - Remember that threads from the ThreadPool are background threads


    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading;

    namespace ThreadPool {
      class Program {
        static void Main(string[] args) {
          Thread t = new Thread(Count);
          t.Start();

          Task task = Task.Run(() => {
            for (int i = 0; i < 8; i++) {
              Thread.Sleep(500);
              Console.Write("BG ");
            }
          });
        }

        static void Count() {
          for (int i = 0; i < 4; i++) {
            Thread.Sleep(500);
            Console.Write("FG ");
          }
        }
      }
    }

## Async and Await 

- Computers are designed to carry out actions synchronously
  - Complete task A, then complete task B
- Asynchronous processing makes this more efficient
  - Task A runs on main thread
  - Task B runs on a separate thread at the same time
- Async is used to mark a method to inform the compiler to look the Await keyword
  - The await keyword tells the compiler that the Async method can't continue past that point until the awaited
    asynchronous process is computed
  - In the meantime, control return to the caller of the Async method
  - Async caller and Await method run simultaneously 


    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace AwaitAndAsync {
      class Program {
        static void Main(string[] args) {
          AsyncAwaitDemo demo = new AsyncAwaitDemo();
          demo.DoStuff();

          for (int i = 0; i < 100; i++) {
            Console.WriteLine("Working on the Main Thread...");
          }
        }
      }

      public class AsyncAwaitDemo {
        public async Task DoStuff() {
          await Task.Run(() => {
            CountToFifty();
          });

          // This will not execute until COuntToFify has completed
          Console.WriteLine("Counting to 50 is completed...");
        }

        private static async Task < string > CountToFifty() {
          int counter;
          for (counter = 0; counter < 51; counter++) {
            Console.WriteLine("BG thread: " + counter);
          }

          return ("Counter = " + counter);
        }
      }

    }