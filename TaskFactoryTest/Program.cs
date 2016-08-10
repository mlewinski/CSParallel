using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskFactoryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts=new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Task<int> t2 = Task<int>.Factory.StartNew((o) =>
            {
                Console.WriteLine(o.ToString());
                return 1;
            }, (object) "Hello world", token, TaskCreationOptions.None, TaskScheduler.Default);

            t2.Wait();
            Console.ReadLine();
        }
    }
}
