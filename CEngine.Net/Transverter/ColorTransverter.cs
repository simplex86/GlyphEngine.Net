using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 颜色转换器
    /// </summary>
    internal static class ColorTransverter
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, ConsoleColor> dict = new()
        {
            { "black", ConsoleColor.Black },
            { "darkblue", ConsoleColor.DarkBlue },
            { "darkgreen", ConsoleColor.DarkGreen },
            { "darkcyan", ConsoleColor.DarkCyan },
            { "darkred", ConsoleColor.DarkRed },
            { "darkmagenta", ConsoleColor.DarkMagenta },
            { "darkyellow", ConsoleColor.DarkYellow },
            { "gray", ConsoleColor.Gray },
            { "darkgray", ConsoleColor.DarkGray },
            { "blue", ConsoleColor.Blue },
            { "green", ConsoleColor.Green },
            { "cyan", ConsoleColor.Cyan },
            { "red", ConsoleColor.Red },
            { "magenta", ConsoleColor.Magenta },
            { "yellow", ConsoleColor.Yellow },
            { "white", ConsoleColor.White },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ConsoleColor Get(string name)
        {
            if (dict.TryGetValue(name, out var color))
            {
                return color;
            }

            return ConsoleColor.White;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ConsoleColor Get(JsonData data, string key)
        {
            if (data.ContainsKey(key))
            {
                var name = (string)data[key];
                return Get(name);
            }

            return ConsoleColor.White;
        }
    }
}
