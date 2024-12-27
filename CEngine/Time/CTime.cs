using System;
using System.Diagnostics;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public static class CTime
    {
        private static Stopwatch watcher = new Stopwatch();
        private static long timestamp = 0;

        /// <summary>
        /// 单位：秒
        /// </summary>
        public static float deltatime { get; private set; } = 0f;

        /// <summary>
        /// 
        /// </summary>
        internal static void Start()
        {
            watcher.Start();
            timestamp = watcher.ElapsedMilliseconds;
        }

        /// <summary>
        /// 
        /// </summary>
        internal static float Update()
        {
            deltatime = (watcher.ElapsedMilliseconds - timestamp) / 1000f;
            timestamp = watcher.ElapsedMilliseconds;

            return deltatime;
        }

        /// <summary>
        /// 
        /// </summary>
        internal static void Stop()
        {
            watcher.Stop();
        }
    }
}
