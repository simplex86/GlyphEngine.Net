using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
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
            if (queue.Count == 0)
            {
                return new CPixel()
                {
                    X = x,
                    Y = y,
                };
            }

            var pixel = queue.Dequeue();
            pixel.X = x;
            pixel.Y = y;

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
