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
        static Random _r = new Random();
        const int ThreadCount = 15;
        static double _pi = 0;
        static void Main(string[] args)
        {
            WaitCallback threadMethod = RunPiComputation;
            ThreadPool.SetMaxThreads(30, 100);
            for(int i = 0; i < ThreadCount; i++)
            {
                ThreadPool.QueueUserWorkItem(threadMethod, i);
            }

            int availableThreadCount = 0;
            int allThreadCount = 0;
            int workingThreadCount = 0;
            int tmp = 0;
            do
            {
                ThreadPool.GetAvailableThreads(out availableThreadCount, out tmp);
                ThreadPool.GetMaxThreads(out allThreadCount, out tmp);
                workingThreadCount = allThreadCount - availableThreadCount;
                Console.WriteLine("Threads working : {0}", workingThreadCount);
                Thread.Sleep(500);
            } while (workingThreadCount > 0);
            Console.WriteLine("PI = {0}, reference = {2}, error = {1}", _pi / ThreadCount, Math.Abs(Math.PI - (_pi / ThreadCount)), Math.PI);
            Console.ReadLine();
        }
        static double CalculatePi(long attemptCount)
        {
            Random r = new Random(Program._r.Next() & DateTime.Now.Millisecond);
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

        static void RunPiComputation(object parameter)
        {
            try
            {
                //int startTime = Environment.TickCount;
                //int endTime = 0;
                long attemptCount = 1000000000L/ThreadCount;
                Console.WriteLine("RunPiComputation MTID:{0}", Thread.CurrentThread.ManagedThreadId);

                double pi = CalculatePi(attemptCount);
                lock ((object)Program._pi) Program._pi += pi;
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
