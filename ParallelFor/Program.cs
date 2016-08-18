using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelFor
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            Stopwatch stoper = new Stopwatch();
            List<int> l = new List<int>();
            int loopCounter = 10000000;
            for(int i = 0; i < loopCounter; i++) l.Add(r.Next()%10000000);
            List<int> lcopy = l.Select(item => item).ToList();
            stoper.Start();
            for (int i = 0; i < loopCounter; i++)
            {
                if (l[i] % 2 != 0)
                {
                    for (int n = 3; n < Math.Sqrt(l[i]); n += 2)
                    {
                        if (l[i]%n == 0) l[i] *= -1;
                    }
                }
            }
            stoper.Stop();
            Console.WriteLine("Sequential loop : {0} ms", stoper.ElapsedMilliseconds);

            stoper.Reset();
            stoper.Start();
            Parallel.ForEach<int>(lcopy, (i) =>
            {
                if (i % 2 != 0)
                {
                    for (int n = 3; n < Math.Sqrt(i); n += 2)
                    {
                        if (i % n == 0) i *= -1;
                    }
                }
            });
            stoper.Stop();
            Console.WriteLine("Parallel loop : {0} ms", stoper.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
