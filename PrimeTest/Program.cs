using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task<int>> tasks = new List<Task<int>>();
            Console.WriteLine("Check if number is prime :");
            int n = Int32.Parse(Console.ReadLine());
            for (int i = 2; i <= (int)Math.Sqrt(n); i++)
            {
                tasks.Add(new Task<int>((j) =>
                {
                    if (n % (int)j == 0) return (int)j;
                    else return 0;
                }, i));
            }
            tasks.ForEach(t => t.Start());
            tasks.ForEach(t => t.Wait());

            bool prime = true;
            foreach (Task<int> task in tasks)
            {
                if (task.Result != 0)
                {
                    Console.WriteLine("{0} is divisible by {1}", n, task.Result);
                    prime = false;
                }
            }
            if (prime) Console.WriteLine("{0} is prime", n);
            Console.ReadLine();
        }
    }
}
