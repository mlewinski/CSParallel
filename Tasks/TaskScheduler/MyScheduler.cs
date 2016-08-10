using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedulerTest
{
    class MyScheduler : TaskScheduler
    {
        private List<Task> queue = new List<Task>();
        private Thread mainThread;

        public MyScheduler()
        {
            mainThread=new Thread(() =>
            {
                Console.WriteLine("Scheduler created");
                while (true)
                {
                    if (queue.Count > 0)
                    {
                        int i = new Random().Next(queue.Count);
                        if(TryExecuteTask(queue[i])) queue.RemoveAt(i);
                    }
                }
            });
            mainThread.IsBackground = true;
            mainThread.Start();
        }

        public override int MaximumConcurrencyLevel { get { return 1;} }

        protected override void QueueTask(Task task)
        {
            queue.Add(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            Console.WriteLine("Sequential execution call");
            if (Thread.CurrentThread != mainThread) return false;
            return TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return queue.ToArray();
        }
    }
}
