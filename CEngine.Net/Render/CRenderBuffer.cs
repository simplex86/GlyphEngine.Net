using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 渲染缓冲
    /// </summary>
    internal class CRenderBuffer
    {
        internal List<CPixel> pixels { get; }

        internal CRenderBuffer()
        {
            pixels = new List<CPixel>(CWorld.width * CWorld.height);
        }

        /// <summary>
        /// 写入像素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal void SetPixel(int x, int y)
        {
            if (!GetPixel(x, y, out var pixel))
            {
                pixel = CPixelPool.Instance.Alloc(x, y);
                pixels.Add(pixel);
            }

            pixel.c = ' ';
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
            if (!GetPixel(x, y, out var pixel))
            {
                pixel = CPixelPool.Instance.Alloc(x, y);
                pixels.Add(pixel);
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
            CPixelPool.Instance.Release(pixels);
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
            pixel = null;

            foreach (var v in pixels)
            {
                if (v.x == x && v.y == y)
                {
                    pixel = v;
                    return true;
                }
            }

            return false;
        }
    }
}
