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
            pixels = new List<CPixel>(Console.BufferWidth * Console.BufferHeight);
        }

        internal void Render()
        {
            if (pixels.Count > 0)
            {
                Console.Clear();
                foreach (var pixel in pixels)
                {
                    Console.SetCursorPosition(pixel.X, pixel.Y);
                    Console.Write(pixel.Value);
                }
                pixels.Clear();
            }
        }

        internal void SetPixel(int x, int y, string value, ConsoleColor color)
        {
            if (!GetPixel(x, y, out var pixel))
            {
                pixel = new CPixel()
                {
                    X = x,
                    Y = y,
                };
                pixels.Add(pixel);
            }

            pixel.Value = value;
            pixel.Color = color;
        }

        internal void Clear()
        {
            pixels.Clear();
        }

        private bool GetPixel(int x, int y, out CPixel pixel)
        {
            pixel = null;

            foreach (var v in pixels)
            {
                if (v.X == x && v.Y == y)
                {
                    pixel = v;
                    return true;
                }
            }

            return false;
        }
    }
}
