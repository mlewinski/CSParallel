using System;
using System.Threading;

namespace MutexTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aplikacja uruchomiona");
            Mutex mutex = new Mutex(false, "mutextest single instance mutex 0x1");
            Console.WriteLine("Mutex created");
            Console.WriteLine();

            bool end = false;

            while (true)
            {
                mutex.WaitOne();
                Console.Write('[');
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter:
                            Console.WriteLine();
                            Console.WriteLine("Thread paused. Press ENTER to release mutex");
                            Console.ReadLine();
                            break;
                        case ConsoleKey.Escape:
                            end = true;
                            break;
                    }
                }

                mutex.ReleaseMutex();
                Console.Write(']');
                if (end)
                {
                    Console.WriteLine("End");
                    return;
                }
                Thread.Sleep(1000);
                Console.Write(" ");
            }
        }
    }
}
