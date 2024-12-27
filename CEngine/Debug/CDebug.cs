using System.Diagnostics;

namespace CEngine
{
    /// <summary>
    /// 调试类
    /// 仅对VisualStudio友好，将文本输出到VisualStudio的Output窗口
    /// </summary>
    public static class CDebug
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        [Conditional("DEBUG")]
        public static void Info(string msg)
        {
            Trace.WriteLine($"{timestamp} [I] {msg}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        [Conditional("DEBUG")]
        public static void Warning(string msg)
        {
            Trace.WriteLine($"{timestamp} [W] {msg}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            Trace.WriteLine($"{timestamp} [E] {msg}");
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        private static string timestamp => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
