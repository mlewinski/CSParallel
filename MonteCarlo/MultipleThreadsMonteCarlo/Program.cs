using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultipleThreadsMonteCarlo
{
    class Program
    {
        static Random r = new Random();
        const int threadCount = 8;
        static double pi = 0;
        static void Main(string[] args)
        {
            Thread[] tt = new Thread[threadCount];
            for(int i = 0; i<threadCount; i++)
            {
                tt[i] = new Thread(RunPiComputation);
                tt[i].Priority = ThreadPriority.AboveNormal;
                tt[i].Start();
            }
            foreach(Thread t in tt)
            {
                t.Join();
                Console.WriteLine("Finished MTID:{0}", t.ManagedThreadId);
            }
            Console.WriteLine("PI = {0}, reference = {2}, error = {1}", pi / threadCount, Math.Abs(Math.PI - (pi / threadCount)), Math.PI);
            Console.ReadLine();
        }
        static double CalculatePi(long attemptCount)
        {
            Random r = new Random(Program.r.Next() & DateTime.Now.Millisecond);
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
            try
            {
                //int startTime = Environment.TickCount;
                //int endTime = 0;
                long attemptCount = 1000000000L/threadCount;
                Console.WriteLine("RunPiComputation MTID:{0}", Thread.CurrentThread.ManagedThreadId);

                double pi = CalculatePi(attemptCount);
                lock ((object)Program.pi) Program.pi += pi;
                Console.WriteLine("Pi = {0}, computation error = {1}", pi, Math.Abs(Math.PI - pi));

                //endTime = Environment.TickCount;
                //Console.WriteLine("Time elapsed : {0}", (endTime - startTime).ToString());
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Thread aborted! MTID:{0}", Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}
