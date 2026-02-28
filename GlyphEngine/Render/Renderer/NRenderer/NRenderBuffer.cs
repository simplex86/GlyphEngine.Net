using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace GlyphEngine
{
    /// <summary>
    /// NRenderBuffer的迭代器
    /// </summary>
    internal class NRenderBufferEnumerator : IEnumerator<CPixel>
    {
        private List<CPixel> list = null;
        private int index = -1;
        private int count = 0;

        /// <summary>
        /// 
        /// </summary>
        public CPixel Current => list[index];

        /// <summary>
        /// 
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        public NRenderBufferEnumerator(List<CPixel> list, int count)
        {
            this.list = list;
            this.count = count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            index++;
            return index < count;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            index = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            index = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        internal void Recount(int count)
        {
            this.index = -1;
            this.count = count;
        }
    }

    /// <summary>
    /// 渲染缓冲
    /// </summary>
    internal class NRenderBuffer : IEnumerable<CPixel>
    {
        private List<CPixel> dense;
        private Dictionary<ulong, int> sparse;
        private int count = 0;
        private NRenderBufferEnumerator enumerator = null;

        /// <summary>
        /// 
        /// </summary>
        internal NRenderBuffer()
        {
            dense = new List<CPixel>(CScreen.Width * CScreen.Height);
            sparse = new Dictionary<ulong, int>();

            enumerator = new NRenderBufferEnumerator(dense, count);
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<CPixel> GetEnumerator() => enumerator;

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 写入像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        internal void SetPixel(int x, int y, char glyph)
        {
            SetPixel(x, y, glyph, Console.ForegroundColor, Console.BackgroundColor);
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
                pixel = new CPixel(x, y);
                AddPixel(key, pixel, count);
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
            count = 0;
            sparse.Clear();

            enumerator?.Recount(count);
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
            var key = CalculateSparseKey(x, y);
            if (GetDenseIndex(key, out var index))
            {
                pixel = dense[index];
                return true;
            }

            pixel = default;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Dispose()
        {
            Clear();
            dense.Clear();

            enumerator?.Dispose();
            enumerator = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pixel"></param>
        /// <param name="index"></param>
        private void AddPixel(ulong key, CPixel pixel, int index)
        {
            dense.Add(pixel);
            AddDenseIndex(key, index);
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
            key = CalculateSparseKey(x, y);
            if (GetDenseIndex(key, out var index))
            {
                pixel = dense[index];
                return true;
            }

            if (count < dense.Count)
            {
                pixel = dense[count];
                pixel.X = x;
                pixel.Y = y;

                AddDenseIndex(key, count);
                return true;
            }

            pixel = default;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private ulong CalculateSparseKey(int x, int y)
        {
            var key = (ulong)x;
            key = (key << 32) | (ulong)y;

            return key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        private void AddDenseIndex(ulong key, int index)
        {
            sparse.Add(key, index);
            count++;

            enumerator?.Recount(count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool GetDenseIndex(ulong key, out int index)
        {
            return sparse.TryGetValue(key, out index);
        }
    }
}
