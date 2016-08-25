using System;
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

            try
            {
                Parallel.For(2, (long) Math.Sqrt(n) + 1, po, (i) =>
                {
                    if (n%i == 0)
                    {
                        cts.Cancel();
                        Console.WriteLine("{0} divides {1}", i, n);
                    }
                    token.ThrowIfCancellationRequested();
                });
                Console.WriteLine("{0} is prime number", n);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("{0} is not prime number", n);
            }
            Console.ReadLine();
        }
    }
}
