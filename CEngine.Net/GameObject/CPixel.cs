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
        public int x { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string symbol { get; set; } = " ";
        /// <summary>
        /// 颜色
        /// </summary>
        public ConsoleColor color { get; set; } = Console.ForegroundColor;
        /// <summary>
        /// 背景颜色
        /// </summary>
        public ConsoleColor backgroundColor { get; set; } = Console.BackgroundColor;

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
            this.x = 0;
            this.y = 0;
            this.symbol = string.Empty;
            this.color = Console.ForegroundColor;
            this.backgroundColor = Console.BackgroundColor;
        }
    }
}
