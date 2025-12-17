namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CPixelPool
    {
        /// <summary>
        /// 
        /// </summary>
        public static CPixelPool Instance { get; } = new CPixelPool();

        /// <summary>
        /// 
        /// </summary>
        private Queue<CPixel> queue = new Queue<CPixel>(1000);

        /// <summary>
        /// 
        /// </summary>
        protected CPixelPool()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public CPixel Alloc(int x, int y)
        {
            var pixel = (queue.Count == 0) ? new CPixel()
                                           : queue.Dequeue();
            pixel.x = x;
            pixel.y = y;

            return pixel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public CPixel Alloc(int x, int y, char c)
        {
            var pixel = Alloc(x, y);
            pixel.c = c;

            return pixel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="c"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public CPixel Alloc(int x, int y, char c, ConsoleColor color)
        {
            var pixel = Alloc(x, y, c);
            pixel.color = color;

            return pixel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pixel"></param>
        public void Release(CPixel pixel)
        {
            pixel.Reset();
            queue.Enqueue(pixel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pixels"></param>
        public void Release(List<CPixel> pixels)
        {
            foreach (var pixel in pixels)
            {
                Release(pixel);
            }
            pixels.Clear();
        }
    }
}
