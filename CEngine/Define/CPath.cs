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
        public static string solutionPath
        {
            get
            {
                if (string.IsNullOrEmpty(_solutionPath))
                {
                    _solutionPath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");
                    if (InEditor())
                    {
                        var idx = _solutionPath.IndexOf("/bin/");
                        if (idx >= 0)
                        {
                            _solutionPath = _solutionPath.Substring(0, idx);
                        }
                    }
                }
                return _solutionPath;
            }
        }

        private static string _solutionPath = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public static string resourcesPath
        {
            get
            {
                if (string.IsNullOrEmpty(_resourcesPath))
                {
                    _resourcesPath = $"{solutionPath}/Resources";
                }
                return _resourcesPath;
            }
        }

        private static string _resourcesPath = string.Empty;

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
