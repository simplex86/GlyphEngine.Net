using System;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 像素
    /// </summary>
    public class CPixel
    {
        /// <summary>
        /// 
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Symbol { get; set; } = " ";
        /// <summary>
        /// 颜色
        /// </summary>
        public ConsoleColor Color { get; set; } = Console.ForegroundColor;
        /// <summary>
        /// 背景颜色
        /// </summary>
        public ConsoleColor BackgroundColor { get; set; } = Console.BackgroundColor;

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            this.X = 0;
            this.Y = 0;
            this.Symbol = string.Empty;
            this.Color = Console.ForegroundColor;
            this.BackgroundColor = Console.BackgroundColor;
        }
    }
}
