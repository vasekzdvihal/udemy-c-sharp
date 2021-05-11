using System;

namespace NullObject.Singleton
{
    public interface ILog
    {
        void Warn();

        public static ILog Null => NullLog.Instance;
        
        private sealed class NullLog : ILog
        {
            private NullLog() {}
            public static ILog Instance => instance.Value;
            private static Lazy<NullLog> instance = new(() => new NullLog());
            public void Warn() {}
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var test = ILog.Null;
        }
    }
}