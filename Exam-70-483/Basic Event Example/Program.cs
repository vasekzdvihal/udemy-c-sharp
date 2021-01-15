using System;

namespace Basic_Event_Example
{
    class Program
    {
        static void Main(string[] args)
        {
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

    public class EvtPublisher
    {
        public EventHandler<EvtArgsClass> evt;

        public void CheckBalance(int x)
        {
            if (x > 250)
            {
                EvtArgsClass eac = new EvtArgsClass("Balance exceeds 250...");
                evt(this, eac);
            }
                
        }
    }

    public class EvtSubscriber
    {
        public void HandleTheEvent(object sender, EvtArgsClass e)
        {
            Console.WriteLine("ATTENTION! " + sender + ": " + e.Message); ;
        }
    }

    public class EvtArgsClass : EventArgs
    {
        public EvtArgsClass(string str)
        {
            msg = str;
        }

        private string msg;
        public string Message 
        {
            get { return msg; }
        }
    }
}
 