using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DivisorsParallelWithCancellationToken
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = Int64.Parse(Console.ReadLine());

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Console.WriteLine("Checking divisors from 2 to {0}", Math.Sqrt(n));
            
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = token;
            bool isPrime = true;
            //try
            //{
                Parallel.ForEach(Partitioner.Create(2, (long) n/2 +1), po, (partition) =>
                {
                    for (long i = partition.Item1; i < partition.Item2; i++)
                    {
                        if (n%i == 0)
                        {
                            isPrime = false;
                            //cts.Cancel();
                            Console.WriteLine("{0} divides {1}", i, n);
                        }
                        //token.ThrowIfCancellationRequested();
                    }
                });
                Console.WriteLine("{0} is {1} prime number", n, isPrime?"":"not");
            //}
            //catch (OperationCanceledException)
            //{
            //    Console.WriteLine("{0} is not prime number", n);
            //}
            Console.ReadLine();
        }
    }
}
