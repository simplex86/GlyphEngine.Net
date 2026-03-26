using System;
using System.Text;

namespace GlyphEngine
{
    /// <summary>
    /// .Net 提供 Console API 虽然跨平台，但效率太低。这个类主要针对不同平台，提高控制台的执行效率。
    /// </summary>
    internal static class CConsoleHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static IntPtr windowsHandle = IntPtr.Zero;
        /// <summary>
        /// 
        /// </summary>
        private static StringBuilder stringBuilder = new StringBuilder();

        /// <summary>
        /// 
        /// </summary>
        static CConsoleHelper()
        {
            var platform = CPlatformHelper.GetPlatform();
            switch (platform)
            {
                case EPlatform.Windows:
                    windowsHandle = CWindowsNativeAPI.GetStdHandle(-11);
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 调用次数最多的是 Console.ForegroundColor 和 Console.ForegroundColor 两个接口。
        /// 如果每次渲染都通过它们来设置颜色，性能很低。
        /// 在 Windows 平台使用 Windows API 大约能提升 60%。但只能解决 Windows 平台下的问题。
        /// 使用 ANSI 转义可以解决跨平台问题，测试后发现性能居然提升 80%
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        public static void SetColor(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            var platform = CPlatformHelper.GetPlatform();
            switch (platform)
            {
                case EPlatform.Windows:
                    if (windowsHandle != IntPtr.Zero)
                    {
                        var attributes = (ushort)(((int)foregroundColor << 4) | (int)backgroundColor);
                        CWindowsNativeAPI.SetConsoleTextAttribute(windowsHandle, attributes);
                    }
                    break;
                default:
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="glyph"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        public static void Write(char glyph, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            SetColor(foregroundColor, backgroundColor);
            {
                Console.Write(glyph);
            }
            SetColor(CScreen.ForegroundColor, CScreen.BackgroundColor);
        }

        /// <summary>
        /// 写（使用 ANSI 转义）
        /// </summary>
        /// <param name="glyph"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        public static void WriteANSI(char glyph, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            stringBuilder.Append("\u001b[38;5;")
                         .Append((byte)foregroundColor)
                         .Append(";48;5;")
                         .Append((byte)backgroundColor)
                         .Append('m')
                         .Append(glyph)
                         .Append("\u001b[0m");

            var text = stringBuilder.ToString();
            Console.Write(text);

            stringBuilder.Clear();
        }
    }
}
