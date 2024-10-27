using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 渲染器
    /// </summary>
    internal class CRenderer
    {
        /// <summary>
        /// 渲染缓存数组
        /// </summary>
        private CRenderBuffer[] buffers = { new CRenderBuffer(), new CRenderBuffer() };
        /// <summary>
        /// 当前帧渲染缓存在缓存数组的索引
        /// </summary>
        private int curIndex = 1;
        /// <summary>
        /// 前一帧渲染缓存在缓存数组的索引
        /// </summary>
        private int preIndex = 0;

        /// <summary>
        /// 渲染缓存：需要被真正渲染的像素缓存区
        /// </summary>
        private CRenderBuffer renderer = new CRenderBuffer();
        /// <summary>
        /// 擦除缓存：需要被擦除的像素缓存区
        /// </summary>
        private CRenderBuffer erasurer = new CRenderBuffer();

        /// <summary>
        /// 当前帧的渲染缓存
        /// </summary>
        private CRenderBuffer current => buffers[curIndex];
        /// <summary>
        /// 前一帧的渲染缓存
        /// </summary>
        private CRenderBuffer previous => buffers[preIndex];

        /// <summary>
        /// 脏标记：是否需要重新渲染当前帧
        /// </summary>
        private bool dirty = false;

        /// <summary>
        /// 往当前帧渲染缓存写入像素数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, string symbol, ConsoleColor color, ConsoleColor backgroundColor)
        {
            dirty = true;
            current.SetPixel(x, y, symbol, color, backgroundColor);
        }

        /// <summary>
        /// 执行渲染
        /// </summary>
        public void Render()
        {
            if (dirty)
            {
                Diff();
                Erase();
                Draw();
                Swap();

                dirty = false;
            }
        }

        /// <summary>
        /// 对比：找到当前帧真正需要被渲染的像素和需要被擦除的像素
        /// </summary>
        private void Diff()
        {
            //
            foreach (var p in current.pixels)
            {
                if (!previous.GetPixel(p.x, p.y, out var q))
                {
                    renderer.SetPixel(p.x, p.y, p.symbol, p.color, p.backgroundColor);
                }
                else if (p.symbol != q.symbol || p.color != q.color)
                {
                    renderer.SetPixel(p.x, p.y, p.symbol, p.color, p.backgroundColor);
                }
            }
            // 
            foreach (var p in previous.pixels)
            {
                if (!current.GetPixel(p.x, p.y, out var q))
                {
                    erasurer.SetPixel(p.x, p.y);
                }
            }
        }

        /// <summary>
        /// 擦除
        /// </summary>
        private void Erase()
        {
            foreach (var pixel in erasurer.pixels)
            {
                DrawPixel(pixel);
            }
            erasurer.Clear();
        }

        /// <summary>
        /// 绘制
        /// </summary>
        private void Draw()
        {
            foreach (var pixel in renderer.pixels)
            {
                DrawPixel(pixel);
            }
            renderer.Clear();
        }

        /// <summary>
        /// 交换缓冲区
        /// </summary>
        private void Swap()
        {
            preIndex = curIndex;
            curIndex = (curIndex + 1) % buffers.Length;

            current.Clear();
        }

        /// <summary>
        /// 绘制像素
        /// </summary>
        /// <param name="pixel"></param>
        private void DrawPixel(CPixel pixel)
        {
            var tuple = SetConsoleColor(pixel);
            {
                Console.SetCursorPosition(pixel.x, pixel.y);
                Console.Write(pixel.symbol);
            }
            ResetConsoleColor(tuple);
        }

        /// <summary>
        /// 设置控制台颜色
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private (ConsoleColor foregroundColor, ConsoleColor backgroundColor) SetConsoleColor(CPixel pixel)
        {
            var foregroundColor = Console.ForegroundColor;
            var backgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = pixel.color;
            Console.BackgroundColor = pixel.backgroundColor;

            return (foregroundColor, backgroundColor);
        }

        /// <summary>
        /// 还原控制台颜色
        /// </summary>
        /// <param name="colors"></param>
        private void ResetConsoleColor(ValueTuple<ConsoleColor, ConsoleColor> colors)
        {
            Console.ForegroundColor = colors.Item1;
            Console.BackgroundColor = colors.Item2;
        }
    }
}
