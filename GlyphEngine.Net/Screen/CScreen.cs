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
        public readonly static int Width = Console.BufferWidth;
        /// <summary>
        /// 
        /// </summary>
        public readonly static int Height = Console.BufferHeight;
        /// <summary>
        /// 世界坐标系中心
        /// </summary>
        internal static CVector2 Center = new CVector2(Width / 2, Height / 2);

        /// <summary>
        /// 
        /// </summary>
        internal static CColor ForegroundColor = CColorHelper.GetColor(Console.ForegroundColor);
        /// <summary>
        /// 
        /// </summary>
        internal static CColor BackgroundColor = CColorHelper.GetColor(Console.BackgroundColor);
    }
}
