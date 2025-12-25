using System;

namespace CEngine
{
    /// <summary>
    /// 通用渲染器
    /// </summary>
    internal class NRenderer : IRenderer
    {
        /// <summary>
        /// 渲染缓存数组
        /// </summary>
        private NRenderBuffer[] buffers = { new NRenderBuffer(), new NRenderBuffer() };
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
        private NRenderBuffer renderer = new NRenderBuffer();
        /// <summary>
        /// 擦除缓存：需要被擦除的像素缓存区
        /// </summary>
        private NRenderBuffer erasurer = new NRenderBuffer();

        /// <summary>
        /// 当前帧的渲染缓存
        /// </summary>
        private NRenderBuffer current => buffers[curIndex];
        /// <summary>
        /// 前一帧的渲染缓存
        /// </summary>
        private NRenderBuffer previous => buffers[preIndex];

        /// <summary>
        /// 往当前帧渲染缓存写入像素数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        /// <param name="backgroundColor"></param>
        public void SetPixel(int x, int y, char glyph, ConsoleColor color, ConsoleColor backgroundColor)
        {
            current.SetPixel(x, y, glyph, color, backgroundColor);
        }

        /// <summary>
        /// 执行渲染
        /// </summary>
        public void Render()
        {
            var dirty = Diff();
            if (dirty) // 有变化时重绘
            {
                Erase();
                Draw();
            }
            Swap();
        }

        /// <summary>
        /// 对比：找到当前帧真正需要被渲染的像素和需要被擦除的像素
        /// </summary>
        private bool Diff()
        {
            var dirty = false;

            renderer.Clear();
            // 被渲染的像素
            foreach (var p in current)
            {
                if (!previous.GetPixel(p.X, p.Y, out var q))
                {
                    renderer.SetPixel(p.X, p.Y, p.Glyph, p.Color, p.BackgroundColor);
                    dirty = true;
                }
                else if (p.Glyph != q.Glyph || p.Color != q.Color)
                {
                    renderer.SetPixel(p.X, p.Y, p.Glyph, p.Color, p.BackgroundColor);
                    dirty = true;
                }
            }
            // 被擦除的像素
            foreach (var p in previous)
            {
                if (!current.GetPixel(p.X, p.Y, out var q))
                {
                    erasurer.SetPixel(p.X, p.Y, CChar.Space);
                    dirty = true;
                }
            }

            return dirty;
        }

        /// <summary>
        /// 擦除
        /// </summary>
        private void Erase()
        {
            foreach (var pixel in erasurer)
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
            foreach (var pixel in renderer)
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
            var context = SetConsoleColor(pixel);
            {
                Console.SetCursorPosition(pixel.X, pixel.Y);
                Console.Write(pixel.Glyph);
            }
            ResetConsoleColor(context);
        }

        /// <summary>
        /// 设置控制台颜色
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private static (ConsoleColor foregroundColor, ConsoleColor backgroundColor) SetConsoleColor(CPixel pixel)
        {
            var foregroundColor = Console.ForegroundColor;
            var backgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = pixel.Color;
            Console.BackgroundColor = pixel.BackgroundColor;

            return (foregroundColor, backgroundColor);
        }

        /// <summary>
        /// 还原控制台颜色
        /// </summary>
        /// <param name="colors"></param>
        private static void ResetConsoleColor(ValueTuple<ConsoleColor, ConsoleColor> colors)
        {
            Console.ForegroundColor = colors.Item1;
            Console.BackgroundColor = colors.Item2;
        }
    }
}
