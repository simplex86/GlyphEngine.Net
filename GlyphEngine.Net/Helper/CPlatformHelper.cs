using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CPlatformHelper
    {
        private static EPlatform platform = EPlatform.Unknown;
        /// <summary>
        /// 
        /// </summary>
        private readonly static Dictionary<OSPlatform, EPlatform> platforms = new Dictionary<OSPlatform, EPlatform>()
        {
            { OSPlatform.Windows,   EPlatform.Windows },
            { OSPlatform.OSX,       EPlatform.Mac },
            { OSPlatform.Linux,     EPlatform.Linux },
        };

        /// <summary>
        /// 
        /// </summary>
        static CPlatformHelper()
        {
            foreach (var kv in platforms)
            {
                if (RuntimeInformation.IsOSPlatform(kv.Key))
                {
                    platform = kv.Value;
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static EPlatform GetPlatform()
        {
            return platform;
        }
    }
}
