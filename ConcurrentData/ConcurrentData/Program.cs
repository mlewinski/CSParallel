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
                l.Add(i);
            });
            Console.WriteLine(cb.Count);
            Console.WriteLine(l.Count);
            Console.ReadLine();
        }
    }
}
