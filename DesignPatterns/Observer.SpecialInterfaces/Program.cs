using System;
using System.Collections.Generic;

namespace Observer.SpecialInterfaces
{
    public class Event
    {
        
    }

    public class FallsIllEvent : Event
    {
        public string Address;
    }

    public class Person : IObservable<Event>
    {
        private class Subscription : IDisposable
        {
            public readonly IObserver<Event> Observer;
            private readonly Person person;
            
            public Subscription(Person person, IObserver<Event> observer)
            {
                this.person = person;
                Observer = observer;
            }
            
            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }

        private readonly HashSet<Subscription> subscriptions = new HashSet<Subscription>();
        
        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this, observer);
            subscriptions.Add(subscription);
            return subscription;
        }

        public void FallIll()
        {
            foreach (var s in subscriptions)
            {
                s.Observer.OnNext(new FallsIllEvent{Address = "123 London Road"});
            }
        }
    }
    
    public class Program : IObserver<Event>
    {
        public static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var person = new Person();
            IDisposable sub = person.Subscribe(this);
            
            person.FallIll();
        }

        public void OnNext(Event value)
        {
            if (value is FallsIllEvent args)
            {
                Console.WriteLine($"A doctor is required at {args.Address}");
            }
        }

        public void OnError(Exception error) { }
        public void OnCompleted() { }
    }
}