using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedulerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyScheduler ts = new MyScheduler();
            TaskFactory tf = new TaskFactory(ts);
            
            int maxTasks = 10;

            Task[] tasks = new Task[maxTasks];


            for (int i = 0; i < maxTasks; i++)
            {
                Console.WriteLine("Task {0} started", i+1);
                tasks[i] = tf.StartNew((number) =>
                {
                    Console.WriteLine("Task {0} finished", number);
                }, i + 1);
            }
            Task.WaitAll(tasks, 20000);
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
    }
}
