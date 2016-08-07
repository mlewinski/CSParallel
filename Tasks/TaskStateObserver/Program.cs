using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskStateObserver
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;
            Action a = () =>
            {
                Console.WriteLine("Task started (id:{0})\n", Task.CurrentId);
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                }
            };
            Task test1 = new Task(() =>
            {
                Console.WriteLine("Task started (id:{0})\n", Task.CurrentId);
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                }
            }, ct, TaskCreationOptions.LongRunning);

            Task test2 = new Task(() =>
            {
                Console.WriteLine("Task started (id:{0})\n", Task.CurrentId);
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                }
            }, TaskCreationOptions.LongRunning);

            Task observer = Task.Factory.StartNew(() =>
            {
                while (!(test1.IsCompleted && test2.IsCompleted))
                {
                    Thread.Sleep(100);
                    Console.WriteLine("Test1 : {0} \t\t Test2 : {1}", test1.Status, test2.Status);
                }
            });

            Thread.Sleep(200);
            test1.Start();
            test2.Start();

            while (test1.Status != TaskStatus.Running || test2.Status != TaskStatus.Running) ;
            Thread.Sleep(200);
            Console.WriteLine("\nInterrupt\n");
            cts.Cancel();

            try
            {
                Task.WaitAll(test1, test2, observer);
            }
            catch (AggregateException ex)
            {
                foreach (var exc in ex.InnerExceptions)
                {
                    Console.WriteLine("Catched exception: {0}", exc.Message);
                }
            }
            Console.ReadLine();
        }
    }
}
