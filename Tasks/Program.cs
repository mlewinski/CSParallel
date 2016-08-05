using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Action a = () =>
            {
                Console.WriteLine("Task {0} start", Task.CurrentId);
                Thread.Sleep(new Random().Next(10000));
                Console.WriteLine("Task {0} finished", Task.CurrentId);
            };

            List<Task> tasks = new List<Task>();
            for(int i=0;i<100;i++) tasks.Add(new Task(a));

            tasks.ForEach(t=>t.Start());
            tasks.ForEach(t=>t.Wait());
        }
    }
}
