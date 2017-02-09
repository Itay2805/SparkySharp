using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Utils
{
    public class Timer
    {

        private Stopwatch stopwatch = new Stopwatch();

        public Timer()
        {
            stopwatch.Start();
        }

        public void Reset()
        {
            stopwatch.Reset();
        }

        public float Elapsed()
        {
            return stopwatch.ElapsedMilliseconds * 0.001f;
        }

        public float ElapsedMillis()
        {
            return stopwatch.ElapsedMilliseconds;
        }

    }
}
