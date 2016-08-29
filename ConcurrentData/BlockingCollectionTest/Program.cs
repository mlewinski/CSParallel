using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlockingCollectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockingCollection<int> bc = new BlockingCollection<int>();

            Action producerAction = () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    bc.Add(i);
                    Console.WriteLine("Added element {0}", i);
                }
                Thread.Sleep(2000);
                bc.CompleteAdding();
            };
            Action consumerAction = () =>
            {
                Thread.Sleep(3000);
                foreach (int i in bc.GetConsumingEnumerable())
                {
                    Console.WriteLine("Taken element {0}", i);
                }
            };

            Parallel.Invoke(producerAction, consumerAction);

            bc = new BlockingCollection<int>(new ConcurrentStack<int>());

            Parallel.Invoke(producerAction, consumerAction);
        }
    }
}
