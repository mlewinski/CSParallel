using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelAggregate
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vals = { 3, 4, 5, 14, 12, 4, 53, 45, 35, 43, 6, 612412, 5, 5454, 63, 1, 431, 5, 63, 636, 9, 6, 67976, 3636, 9, 0, 786, 67, 4 };

            double avg = vals.AsParallel().Average();

            double[] coords = { 1293, 1294.13 };
            double distance = coords.AsParallel()
                .Aggregate(0, (double sum, double i) => sum + i * i, (sum1, sum2) => sum1 + sum2, (result) => Math.Sqrt((double)result));

            Console.WriteLine("Average : {0}", avg);

            Console.WriteLine("Distance : {0}", distance);


            //Pi 

            Console.WriteLine("PI = {0}", Math.PI);

            const int range = 20000000;
            Func<int, double> sign = (i) =>
            {
                return (i%2 == 1) ? -1 : 1;
            };
            Func<int, double> odd = (i) =>
            {
                return (double) 2*i + 1;
            };
            Func<int, double> series = (i) =>
            {
                return sign(i)/odd(i);
            };
            IEnumerable<double> query = from i in new ConcurrentBag<int>(Enumerable.Range(0, range)) select series(i);

            var time = Environment.TickCount;
            var result1 = 4*query.Sum();
            time = Environment.TickCount - time;
            Console.WriteLine("Sequential SUM : PI = {0} | {1} ms", result1, time);

            time = Environment.TickCount;
            var result2 = 4*query.AsParallel().Sum();
            time = Environment.TickCount - time;
            Console.WriteLine("Parallel SUM : PI = {0} | {1} ms", result2, time);

            time = Environment.TickCount;
            var result3=4*new ConcurrentBag<int>(Enumerable.Range(0,range)).Aggregate<int,double,double>(0.0,(sum,i)=>sum+series(i), (sum)=>sum);
            time = Environment.TickCount - time;
            Console.WriteLine("Sequential AGGREGATE : PI = {0} | {1} ms", result3, time);

            time = Environment.TickCount;
            var result4 = 4 * new ConcurrentBag<int>(Enumerable.Range(0, range)).AsParallel().Aggregate(0.0, (sum, i) => sum + series(i),(sum1,sum2)=>sum1+sum2, (sum) => sum);
            time = Environment.TickCount - time;
            Console.WriteLine("Parallel AGGREGATE : PI = {0} | {1} ms", result4, time);
        }
    }
}
