using System;

namespace Singleton.Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            static bool IsSingleton(Func<object> func)
            {
                var instance1 = func();
                var instance2 = func();
                return ReferenceEquals(instance1, instance2);
            }
        }
    }
}