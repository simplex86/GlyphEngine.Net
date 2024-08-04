using System;
using System.Diagnostics;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CTimeImp
    {
        private Stopwatch watcher = new Stopwatch();
        private long timestamp = 0;

        /// <summary>
        /// 单位：秒
        /// </summary>
        public float deltatime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal void Start()
        {
            watcher.Start();
            timestamp = watcher.ElapsedMilliseconds;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Update()
        {
            deltatime = (watcher.ElapsedMilliseconds - timestamp) / 1000f;
            timestamp = watcher.ElapsedMilliseconds;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Stop()
        {
            watcher.Stop();
        }
    }
}
