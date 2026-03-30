using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GlyphEngine
{
    /// <summary>
    /// NRenderBuffer的迭代器
    /// </summary>
    internal class CRendererBufferEnumerator : IEnumerator<CPixel>, IDisposable
    {
        private List<CPixel> buffer = null;
        private int index = -1;

        /// <summary>
        /// 
        /// </summary>
        public CPixel Current => buffer[index];

        /// <summary>
        /// 
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        public CRendererBufferEnumerator(List<CPixel> list)
        {
            this.buffer = list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            index++;
            return index < buffer.Count;
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
    internal class CRendererBuffer : IEnumerable<CPixel>, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private int width;
        /// <summary>
        /// 
        /// </summary>
        private int height;
        /// <summary>
        /// 稀疏数组
        /// </summary>
        private int[] sparse;
        /// <summary>
        /// 紧密数组
        /// </summary>
        private List<CPixel> dense;
        /// <summary>
        /// 迭代器
        /// </summary>
        private CRendererBufferEnumerator enumerator = null;

        /// <summary>
        /// 
        /// </summary>
        internal CRendererBuffer()
            : this(CScreen.Width, CScreen.Height)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        internal CRendererBuffer(int width, int height)
        {
            this.width = width;
            this.height = height;

            sparse = new int[width * height];
            sparse.AsSpan().Fill(-1);

            dense = new List<CPixel>(width * height);
            enumerator = new CRendererBufferEnumerator(dense);
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
            SetPixel(x, y, glyph, CScreen.ForegroundColor, CScreen.BackgroundColor);
        }

        /// <summary>
        /// 写入像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        /// <param name="backgroundColor"></param>
        internal void SetPixel(int x, int y, char glyph, CColor color, CColor backgroundColor)
        {
            if (TryGetIndex(x, y, out var key, out var index))
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
            sparse.AsSpan().Fill(-1);
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
            if (TryGetIndex(x, y, out var _, out var index))
            {
                pixel = CollectionsMarshal.AsSpan(dense)[index];
                return true;
            }

            pixel = default;
            return false;
        }

        /// <summary>
        /// 获取xy坐标对应的索引值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="key">稀疏数组的索引值</param>
        /// <param name="index">紧密数组的索引值</param>
        /// <returns></returns>
        private bool TryGetIndex(int x, int y, out int key, out int index)
        {
            key = y * width + x;
            index = sparse[key];

            return index >= 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Clear();

            enumerator?.Dispose();
            enumerator = null;
        }
    }
}
