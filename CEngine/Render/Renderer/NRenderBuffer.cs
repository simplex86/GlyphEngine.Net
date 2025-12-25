using System;
using System.Collections;
using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 渲染缓冲
    /// </summary>
    internal class NRenderBuffer : IEnumerable<CPixel>
    {
        private List<CPixel> list;
        private Dictionary<ulong, int> grid;

        internal NRenderBuffer()
        {
            list = new List<CPixel>(CScreen.Width * CScreen.Height);
            grid = new Dictionary<ulong, int>();
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<CPixel> GetEnumerator() => list.GetEnumerator();

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

        /// <summary>
        /// 写入像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        internal void SetPixel(int x, int y, char glyph)
        {
            if (!GetPixel(x, y, out var key, out var pixel))
            {
                pixel = CPixelPool.Instance.Alloc(x, y);
                list.Add(pixel);
                grid.Add(key, list.Count - 1);
            }

            pixel.Glyph = glyph;
            pixel.Color = Console.ForegroundColor;
        }

        /// <summary>
        /// 写入像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        /// <param name="backgroundColor"></param>
        internal void SetPixel(int x, int y, char glyph, ConsoleColor color, ConsoleColor backgroundColor)
        {
            if (!GetPixel(x, y, out var key, out var pixel))
            {
                pixel = CPixelPool.Instance.Alloc(x, y);
                list.Add(pixel);
                grid.Add(key, list.Count - 1);
            }

            pixel.Glyph = glyph;
            pixel.Color = color;
            pixel.BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// 清空
        /// </summary>
        internal void Clear()
        {
            CPixelPool.Instance.Release(list);
            grid.Clear();
        }

        /// <summary>
        /// 获取像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        internal bool GetPixel(int x, int y, out CPixel pixel)
        {
            return GetPixel(x, y, out var _, out pixel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="key"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private bool GetPixel(int x, int y, out ulong key, out CPixel pixel)
        {
            pixel = null;

            key = (ulong)x;
            key = (key << 32) | (ulong)y;

            if (grid.TryGetValue(key, out var index))
            {
                pixel = list[index];
                return true;
            }

            return false;
        }
    }
}
