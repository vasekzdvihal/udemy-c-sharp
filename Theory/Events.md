# Events

## What is Event?

- Events enable a class or object to notify other classes or objects when something of interest occurs
    - The class that sends (or raises) the event is called the PUBLISHER
    - The class that receive (or handle) the event are called SUBSCRIBERS
- Events have the following characteristics...
    - The publisher determines when an event is raised
    - The subscribers determine what action is taken in response to the event
    - An event can have multiple subscribers
    - A subscriber can handle multiple events from multiple publishers
    - Events are typically used to signal user actions
    - Events are based on the EventHandler delegate and the EventArgs base class

## Using EventHandler

- The .NET Framework provides an event that is pre-wired to a delegate
    - The EventHandler event
- Let's make our event example

## Using EventArgs

- Custom event arguments can be used to make events more meaningfully
    - Generics make is easier to implement

``` csharp
    using System;

    namespace Basic_Event_Example {
      class Program {
        static void Main(string[] args) {
          // Instatiate an event publisher object
          EvtPublisher ep = new EvtPublisher();

          // Instatiate an event subscriber object
          EvtSubscriber es = new EvtSubscriber();
          ep.evt += es.HandleTheEvent;

          // Call the CheckBalance method on the ep object
          // it will invoke th ep.evt delegate if the balance exceeds 250
          ep.CheckBalance(260);
        }
      }

      public class EvtPublisher {
        public EventHandler < EvtArgsClass > evt;

        public void CheckBalance(int x) {
          if (x > 250) {
            EvtArgsClass eac = new EvtArgsClass("Balance exceeds 250...");
            evt(this, eac);
          }

        }
      }

      public class EvtSubscriber {
        public void HandleTheEvent(object sender, EvtArgsClass e) {
          Console.WriteLine("ATTENTION! " + sender + ": " + e.Message);;
        }
      }

      public class EvtArgsClass: EventArgs {
        public EvtArgsClass(string str) {
          msg = str;
        }

        private string msg;
        public string Message {
          get {
            return msg;
          }
        }
      }
    }
```