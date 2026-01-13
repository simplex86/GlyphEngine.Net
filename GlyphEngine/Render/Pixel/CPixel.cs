using System;

namespace GlyphEngine
{
    /// <summary>
    /// 像素
    /// </summary>
    public class CPixel
    {
        /// <summary>
        /// 
        /// </summary>
        internal int X { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal int Y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public char Glyph { get; set; } = CGlyph.Empty;
        /// <summary>
        /// 颜色
        /// </summary>
        public ConsoleColor Color { get; set; } = Console.ForegroundColor;
        /// <summary>
        /// 背景颜色
        /// </summary>
        public ConsoleColor BackgroundColor { get; set; } = Console.BackgroundColor;

        /// <summary>
        /// 
        /// </summary>
        internal CPixel()
        {

        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            this.X = 0;
            this.Y = 0;
            this.Glyph = CGlyph.Empty;
            this.Color = Console.ForegroundColor;
            this.BackgroundColor = Console.BackgroundColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal CPixel Clone()
        {
            var clone = CPixelPool.Instance.Alloc(X, Y, Glyph, Color);
            return clone;
        }
    }
}
