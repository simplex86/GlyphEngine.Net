using System;
using System.Collections.Generic;
using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 键值转换器
    /// </summary>
    internal static class CKeycodeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, ConsoleKey> dict = new()
        {
            { "backspace", ConsoleKey.Backspace },
            { "tab", ConsoleKey.Tab },
            { "clear", ConsoleKey.Clear },
            { "enter", ConsoleKey.Enter },
            { "escape", ConsoleKey.Escape },
            { "spacebar", ConsoleKey.Spacebar },
            { "pageup", ConsoleKey.PageUp },
            { "pagedown", ConsoleKey.PageDown },
            { "end", ConsoleKey.End },
            { "home", ConsoleKey.Home },
            { "leftarrow", ConsoleKey.LeftArrow },
            { "uparrow", ConsoleKey.UpArrow },
            { "rightarrow", ConsoleKey.RightArrow },
            { "downarrow", ConsoleKey.DownArrow },
            { "a", ConsoleKey.A },
            { "b", ConsoleKey.B },
            { "c", ConsoleKey.C },
            { "d", ConsoleKey.D },
            { "e", ConsoleKey.E },
            { "f", ConsoleKey.F },
            { "g", ConsoleKey.G },
            { "h", ConsoleKey.H },
            { "i", ConsoleKey.I },
            { "j", ConsoleKey.J },
            { "k", ConsoleKey.K },
            { "l", ConsoleKey.L },
            { "m", ConsoleKey.M },
            { "n", ConsoleKey.N },
            { "o", ConsoleKey.O },
            { "p", ConsoleKey.P },
            { "q", ConsoleKey.Q },
            { "r", ConsoleKey.R },
            { "s", ConsoleKey.S },
            { "t", ConsoleKey.T },
            { "u", ConsoleKey.U },
            { "v", ConsoleKey.V },
            { "w", ConsoleKey.W },
            { "x", ConsoleKey.X },
            { "y", ConsoleKey.Y },
            { "z", ConsoleKey.Z },
            { "0", ConsoleKey.D0 },
            { "1", ConsoleKey.D1 },
            { "2", ConsoleKey.D2 },
            { "3", ConsoleKey.D3 },
            { "4", ConsoleKey.D4 },
            { "5", ConsoleKey.D5 },
            { "6", ConsoleKey.D6 },
            { "7", ConsoleKey.D7 },
            { "8", ConsoleKey.D8 },
            { "9", ConsoleKey.D9 },
            { "n0", ConsoleKey.NumPad0 },
            { "n1", ConsoleKey.NumPad1 },
            { "n2", ConsoleKey.NumPad2 },
            { "n3", ConsoleKey.NumPad3 },
            { "n4", ConsoleKey.NumPad4 },
            { "n5", ConsoleKey.NumPad5 },
            { "n6", ConsoleKey.NumPad6 },
            { "n7", ConsoleKey.NumPad7 },
            { "n8", ConsoleKey.NumPad8 },
            { "n9", ConsoleKey.NumPad9 },
            { "f1", ConsoleKey.F1 },
            { "f2", ConsoleKey.F2 },
            { "f3", ConsoleKey.F3 },
            { "f4", ConsoleKey.F4 },
            { "f5", ConsoleKey.F5 },
            { "f6", ConsoleKey.F6 },
            { "f7", ConsoleKey.F7 },
            { "f8", ConsoleKey.F8 },
            { "f9", ConsoleKey.F9 },
            { "f10", ConsoleKey.F10 },
            { "f11", ConsoleKey.F11 },
            { "f12", ConsoleKey.F12 },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ConsoleKey Get(string name)
        {
            if (dict.TryGetValue(name, out var keycode))
            {
                return keycode;
            }

            return ConsoleKey.Escape;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ConsoleKey Get(JsonData data, string key)
        {
            if (data.AsString(key, out var name))
            {
                return Get(name);
            }

            return ConsoleKey.Escape;
        }
    }
}
