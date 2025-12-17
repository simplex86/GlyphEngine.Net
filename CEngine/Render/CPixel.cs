namespace CEngine
{
    /// <summary>
    /// 像素
    /// </summary>
    public class CPixel
    {
        /// <summary>
        /// 
        /// </summary>
        internal int x { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal int y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public char c { get; set; } = CChar.Empty;
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
            this.c = CChar.Empty;
            this.color = Console.ForegroundColor;
            this.backgroundColor = Console.BackgroundColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal CPixel Clone()
        {
            var clone = CPixelPool.Instance.Alloc(x, y, c, color);
            return clone;
        }
    }
}
