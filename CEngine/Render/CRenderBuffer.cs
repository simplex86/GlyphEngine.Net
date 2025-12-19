using System;
using System.Collections;
using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 渲染缓冲
    /// </summary>
    internal class CRenderBuffer : IEnumerable<CPixel>
    {
        private List<CPixel> list;
        private Dictionary<ulong, int> grid;

        internal CRenderBuffer()
        {
            list = new List<CPixel>(CWorld.width * CWorld.height);
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
        internal void SetPixel(int x, int y, char c)
        {
            if (!GetPixel(x, y, out var key, out var pixel))
            {
                pixel = CPixelPool.Instance.Alloc(x, y);
                list.Add(pixel);
                grid.Add(key, list.Count - 1);
            }

            pixel.c = c;
            pixel.color = Console.ForegroundColor;
        }

        /// <summary>
        /// 写入像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        internal void SetPixel(int x, int y, char c, ConsoleColor color, ConsoleColor backgroundColor)
        {
            if (!GetPixel(x, y, out var key, out var pixel))
            {
                pixel = CPixelPool.Instance.Alloc(x, y);
                list.Add(pixel);
                grid.Add(key, list.Count - 1);
            }

            pixel.c = c;
            pixel.color = color;
            pixel.backgroundColor = backgroundColor;
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
