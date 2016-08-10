using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RelayRace
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t1, t2, t11, t22;
            Random random=new Random();
            Action a = () =>
            {
                Console.WriteLine("Runner {0} started", Task.CurrentId);
                Thread.Sleep(random.Next(5000, 12000));
                Console.WriteLine("Runner {0} finished the run", Task.CurrentId);
            };

            Action<Task> b = (t) =>
            {
                Console.WriteLine("Runner {0} started after {1}",Task.CurrentId,t.Id);
                Thread.Sleep(random.Next(5000,12500));
            Console.WriteLine("Runner {0} finished the race", Task.CurrentId);
            };
            t11 = (t1 = Task.Factory.StartNew(a)).ContinueWith(b);
            t22 = (t2 = Task.Factory.StartNew(a)).ContinueWith(b);

            Task[] tasks = {t11, t22};
            Task.Factory.ContinueWhenAny(tasks,
                (t) =>
                {
                    Console.WriteLine("Runner {0} won the race", t.Id);
                });
            Task.WaitAll(tasks);
            Console.WriteLine("Race finished");
            Console.ReadLine();
        }
    }
}
