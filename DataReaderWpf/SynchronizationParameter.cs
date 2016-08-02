using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReaderWpf
{
    class SynchronizationParameter
    {
        public int Progress { get; set; }
        public int Value { get; set; }

        public SynchronizationParameter(int progress, int value)
        {
            this.Progress = progress;
            this.Value = value;
        }
    }
}
