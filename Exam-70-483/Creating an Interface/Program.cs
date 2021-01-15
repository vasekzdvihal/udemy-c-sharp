namespace Creating_an_Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            Machine1 m1 = new Machine1();
            Machine2 m2 = new Machine2();
            Machine3 m3 = new Machine3();

            m1.Start();
            m1.Stop();
            m2.Start();
            m2.Stop();                
            m3.Start();
            m3.Stop();
        }
    }

    public class Machine1 : IControls
    {
        public void Start()
        {
            // add code
        }

        public void Stop()
        {
            // add code
        }
    }

    public class Machine2 : IControls
    {
        public void Start()
        {
            // add code
        }

        public void Stop()
        {
            // add code
        }
    }

    public class Machine3 : IControls
    {
        public void Start()
        {
            // add code
        }

        public void Stop()
        {
            // add code
        }
    }

    interface IControls
    {
        void Start();
        void Stop();
    }
}
