using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 10, (i) => { Console.WriteLine("{0} : {1}", i, Task.CurrentId); });
            Console.ReadLine();
        }
    }
}
