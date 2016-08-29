using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentData
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentBag<int> cb = new ConcurrentBag<int>();
            List<int> l = new List<int>();
            Parallel.For(0, 10000, (i) =>
            {
                cb.Add(i);
                //l.Add(i); //throws AggregateException
            });
            Console.WriteLine(cb.Count);
            Console.WriteLine(l.Count);

            int size = 10;
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>();
            ConcurrentStack<int> stack = new ConcurrentStack<int>();

            for (int i = 0; i < size; i++)
            {
                queue.Enqueue(i);
                stack.Push(i);
            }

            int element;
            Console.WriteLine("Items from queue : ");
            string s = "";
            Parallel.For(0, size, (i) =>
            {
                queue.TryDequeue(out element);
                s += element.ToString() + " ";
            });
            Console.WriteLine(s);
            Console.WriteLine("Items from stack : ");
            s = "";
            Parallel.For(0, size, (i) =>
            {
                stack.TryPop(out element);
                s += element.ToString() + " ";
            });
            Console.WriteLine(s);
        }
    }
}
