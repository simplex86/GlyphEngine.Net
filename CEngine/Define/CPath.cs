using System;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CPath
    {
        /// <summary>
        /// 
        /// </summary>
        public static string solution
        {
            get
            {
                if (string.IsNullOrEmpty(_solution))
                {
                    _solution = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");
                    if (InEditor())
                    {
                        var idx = _solution.IndexOf("/bin/");
                        if (idx >= 0)
                        {
                            _solution = _solution.Substring(0, idx);
                        }
                    }
                }
                return _solution;
            }
        }

        private static string _solution = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public static string resources
        {
            get
            {
                if (string.IsNullOrEmpty(_resources))
                {
                    _resources = $"{solution}/Resources";
                }
                return _resources;
            }
        }

        private static string _resources = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static bool InEditor()
        {
            return System.Diagnostics.Debugger.IsAttached;
        }
    }
}
