using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokenTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            Task t = new Task(() =>
             {
                 try
                 {
                     Console.WriteLine("Task started");
                     for (;;) ct.ThrowIfCancellationRequested();
                 }
                 catch (OperationCanceledException) { Console.WriteLine("Task canceled"); }
             }, ct);

            t.Start();
            Thread.Sleep(3000);
            cts.Cancel();
            t.Wait();
            Console.ReadLine();
        }
    }
}
