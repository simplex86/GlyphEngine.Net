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
        public static int Width => Console.BufferWidth;
        /// <summary>
        /// 
        /// </summary>
        public static int Height => Console.BufferHeight;
        /// <summary>
        /// 世界坐标系中心
        /// </summary>
        internal static CVector2 Center { get; } = new CVector2(Width / 2, Height / 2);
    }
}
