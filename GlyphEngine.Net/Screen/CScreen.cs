using System;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    public static class CScreen
    {
        /// <summary>
        /// 
        /// </summary>
        public static int Width { get; } = Console.BufferWidth;
        /// <summary>
        /// 
        /// </summary>
        public static int Height { get; } = Console.BufferHeight;
        /// <summary>
        /// 世界坐标系中心
        /// </summary>
        internal static CVector2 Center { get; } = new CVector2(Width / 2, Height / 2);

        /// <summary>
        /// 
        /// </summary>
        internal static ConsoleColor ForegroundColor { get; } = Console.ForegroundColor;
        /// <summary>
        /// 
        /// </summary>
        internal static ConsoleColor BackgroundColor { get; } = Console.BackgroundColor;
    }
}
