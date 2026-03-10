using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GlyphEngine
{
    /// <summary>
    /// NRenderBuffer的迭代器
    /// </summary>
    internal class NRendererBufferEnumerator : IEnumerator<CPixel>
    {
        private List<CPixel> list = null;
        private int index = -1;

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
        public NRendererBufferEnumerator(List<CPixel> list, int count)
        {
            this.list = list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            index++;
            return index < list.Count;
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
    }

    /// <summary>
    /// 渲染缓冲
    /// </summary>
    internal class NRendererBuffer : IEnumerable<CPixel>
    {
        private List<CPixel> dense;
        private Dictionary<ulong, int> sparse;
        private NRendererBufferEnumerator enumerator = null;

        /// <summary>
        /// 
        /// </summary>
        internal NRendererBuffer()
        {
            dense = new List<CPixel>(CScreen.Width * CScreen.Height);
            sparse = new Dictionary<ulong, int>();

            enumerator = new NRendererBufferEnumerator(dense, 0);
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
            var key = Key(x, y);
            if (sparse.TryGetValue(key, out var index))
            {
                var span = CollectionsMarshal.AsSpan(dense);
                span[index].Glyph = glyph;
                span[index].Color = color;
                span[index].BackgroundColor = backgroundColor;
            }
            else
            {
                dense.Add(new CPixel(x, y, glyph, color, backgroundColor));
                sparse[key] = dense.Count - 1;
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        internal void Clear()
        {
            dense.Clear();
            sparse.Clear();
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
            var key = Key(x, y);
            if (sparse.TryGetValue(key, out var index))
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

            enumerator?.Dispose();
            enumerator = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private ulong Key(int x, int y)
        {
            var key = (ulong)x;
            key = (key << 32) | (ulong)y;

            return key;
        }
    }
}
