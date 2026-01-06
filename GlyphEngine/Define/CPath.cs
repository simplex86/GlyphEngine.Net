using System;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CPath
    {
        /// <summary>
        /// 
        /// </summary>
        public static string Solution
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
        public static string Resources
        {
            get
            {
                if (string.IsNullOrEmpty(_resources))
                {
                    _resources = $"{Solution}/Resources";
                }
                return _resources;
            }
        }

        private static string _resources = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string Combine(string a, string b)
        {
            if (!a.EndsWith("\\") && !a.EndsWith("/"))
            {
                a = a + "/";
            }
            if (b.StartsWith("\\") || b.StartsWith("/"))
            {
                b = b.Substring(1);
            }

            return (a + b).Replace("\\", "/");
        }

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
