using System;
using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 颜色转换器
    /// </summary>
    internal static class CColorHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<ConsoleColor, CColor> dict = new()
        {
            { ConsoleColor.Black,       CColor.Black },
            { ConsoleColor.DarkBlue,    CColor.DarkBlue },
            { ConsoleColor.DarkGreen,   CColor.DarkGreen },
            { ConsoleColor.DarkCyan,    CColor.DarkCyan },
            { ConsoleColor.DarkRed,     CColor.DarkRed },
            { ConsoleColor.DarkMagenta, CColor.DarkMagenta },
            { ConsoleColor.DarkYellow,  CColor.DarkYellow },
            { ConsoleColor.Gray,        CColor.Gray },
            { ConsoleColor.DarkGray,    CColor.DarkGray },
            { ConsoleColor.Blue,        CColor.Blue },
            { ConsoleColor.Green,       CColor.Green },
            { ConsoleColor.Cyan,        CColor.Cyan },
            { ConsoleColor.Red,         CColor.Red },
            { ConsoleColor.Magenta,     CColor.Magenta },
            { ConsoleColor.Yellow,      CColor.Yellow },
            { ConsoleColor.White,       CColor.White },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ConsoleColor GetClosestConsoleColor(CColor color)
        {
            return GetClosestConsoleColor(color.R, color.G, color.B);
        }

        /// <summary>
        /// RGB
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ConsoleColor GetClosestConsoleColor(byte r, byte g, byte b)
        {
            var z = ConsoleColor.Black;
            double u = r, v = g, w = b, d = double.MaxValue;

            foreach (var kv in dict)
            {
                var k = kv.Key;
                var c = kv.Value;

                var t = Math.Pow(c.R - u, 2.0) + Math.Pow(c.G - v, 2.0) + Math.Pow(c.B - w, 2.0);
                if (t == 0.0) return k;

                if (t < d)
                {
                    d = t;
                    z = k;
                }
            }

            return z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consoleColor"></param>
        /// <returns></returns>
        public static CColor GetColor(ConsoleColor consoleColor)
        {
            if (dict.TryGetValue(consoleColor, out var color))
            {
                return color;
            }

            return CColor.Black;
        }
    }
}
