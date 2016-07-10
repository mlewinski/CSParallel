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
        const int ThreadCount = 20;
        static double _pi = 0;
        static long AttemptCount = 10000000L;
        static long Hits = 0;
        static EventWaitHandle[] ewht = new EventWaitHandle[ThreadCount];

        static void Main(string[] args)
        {
            WaitCallback threadMethod = RunPiComputation;
            ThreadPool.SetMaxThreads(30, 100);
            for (int i = 0; i < ThreadCount; i++)
            {
                ewht[i] = new EventWaitHandle(false, EventResetMode.AutoReset);
                ThreadPool.QueueUserWorkItem(threadMethod, i);
            }
            Thread timer = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Console.WriteLine("Progress : {0}/{1}", Interlocked.Read(ref Program.Hits), Program.AttemptCount * ThreadCount);
                        Thread.Sleep(500);
                    }
                }
                catch (ThreadAbortException)
                {
                    Console.WriteLine("Progress : {0}/{1}", Program.Hits, Program.AttemptCount * ThreadCount);
                }
            });
            timer.Priority=ThreadPriority.Highest;
            timer.IsBackground = true;
            timer.Start();
            for (int i = 0; i < ThreadCount; i++) ewht[i].WaitOne();
            timer.Abort();
            Thread.Sleep(200);
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
                Interlocked.Add(ref Program.Hits, 1);
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
                int? indeks = parameter as int?;
                //int startTime = Environment.TickCount;
                //int endTime = 0;
                long attemptCount = Program.AttemptCount / ThreadCount;
                Console.WriteLine("RunPiComputation MTID:{0} ATI:{1}", Thread.CurrentThread.ManagedThreadId, indeks);

                double pi = CalculatePi(attemptCount);
                lock ((object)Program._pi) Program._pi += pi;
                Console.WriteLine("Pi = {0}, computation error = {1} MTID:{2} ATI:{3}", pi, Math.Abs(Math.PI - pi), Thread.CurrentThread.ManagedThreadId, indeks);
                ewht[indeks.Value].Set();
                //endTime = Environment.TickCount;
                //Console.WriteLine("Time elapsed : {0}", (endTime - startTime).ToString());
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Thread aborted! MTID:{0}", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in MTID:{0} Message : {1}", Thread.CurrentThread.ManagedThreadId, ex.Message);
            }
        }
    }
}
