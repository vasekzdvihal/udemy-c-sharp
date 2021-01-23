using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Factory.AsynchronousMethod
{
    public class Foo
    {
        private Foo()
        {
            // 
        }

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Foo x = await Foo.CreateAsync();
        }
    }
}
