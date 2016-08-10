using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisorsParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = 0;
            while (true)
            {
                Console.Write("Input number : ");
                try
                {
                    n = Int64.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                break;
            }

            List<long> divisors = new List<long>();

            bool prime = true;
            Parallel.For( 2L, (long) n/2 + 1, (long i) =>
            {
                if (n % (long) i == 0)
                {
                    Console.WriteLine("{0} is divisible by {1}", n, i);
                    divisors.Add(i);
                    prime = false;
                }
            });
            divisors.Sort();
            if(divisors.Count>0) foreach(long x in divisors) Console.Write(x + " ");
            if(prime) Console.WriteLine("{0} is prime number", n);
            Console.ReadLine();

        }
    }
}
