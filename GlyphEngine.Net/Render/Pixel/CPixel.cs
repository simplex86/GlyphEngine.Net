using System;

namespace GlyphEngine
{
    /// <summary>
    /// 像素
    /// </summary>
    public struct CPixel
    {
        /// <summary>
        /// 
        /// </summary>
        public int X { get; internal set; }
        /// <summary>
        /// 
        /// </summary>
        public int Y { get; internal set; }
        /// <summary>
        /// 
        /// </summary>
        public char Glyph = CGlyph.Empty;
        /// <summary>
        /// 颜色
        /// </summary>
        public ConsoleColor Color = CScreen.ForegroundColor;
        /// <summary>
        /// 背景颜色
        /// </summary>
        public ConsoleColor BackgroundColor = CScreen.BackgroundColor;

        /// <summary>
        /// 
        /// </summary>
        public readonly static CPixel Default = new CPixel();

        /// <summary>
        /// 
        /// </summary>
        public CPixel()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public CPixel(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        public CPixel(int x, int y, char glyph)
            : this(x, y)
        {
            Glyph = glyph;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        public CPixel(int x, int y, char glyph, ConsoleColor color)
            : this(x, y, glyph)
        {
            Color = color;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        /// <param name="backgroundColor"></param>
        public CPixel(int x, int y, char glyph, ConsoleColor color, ConsoleColor backgroundColor)
            : this(x, y, glyph, color)
        {
            BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        public void Set(char glyph, ConsoleColor color)
        {
            this.Glyph = glyph;
            this.Color = color;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        /// <param name="backgroundColor"></param>
        public void Set(char glyph, ConsoleColor color, ConsoleColor backgroundColor)
        {
            this.Glyph = glyph;
            this.Color = color;
            this.BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal CPixel Clone()
        {
            var clone = new CPixel(X, Y, Glyph, Color);
            return clone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        internal bool Equals(CPixel other)
        {
            if (Glyph == other.Glyph &&
                Color == other.Color &&
                BackgroundColor == other.BackgroundColor)
            {
                return true;
            }

            return false;
        }
    }
}
