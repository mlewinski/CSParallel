using System;
using System.Threading;

namespace SeparateThreadMonteCarlo
{
    class Program
    {
        static Random r = new Random();
        static void Main(string[] args)
        {
            Thread t = new Thread(RunPiComputation);
            t.Start();
            Console.ReadLine();
        }

        static double CalculatePi(long attemptCount)
        {
            double x, y;
            long hitCount = 0;
            for (long i = 0; i < attemptCount; i++)
            {
                x = r.NextDouble();
                y = r.NextDouble();
                if (x * x + y * y < 1) ++hitCount;
                //Console.WriteLine("Hit at x={0} y={1}", x, y);
            }
            return 4.0 * hitCount / attemptCount;
        }

        static void RunPiComputation()
        {
            int startTime = Environment.TickCount;

            long attemptCount = 100000000L;
            double pi = CalculatePi(attemptCount);
            Console.WriteLine("Pi = {0}, computation error = {1}", pi, Math.Abs(Math.PI - pi));

            int endTime = Environment.TickCount;

            Console.WriteLine("Time elapsed : {0}", (endTime - startTime).ToString());
        }
    }
}