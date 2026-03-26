using GlyphEngine.Net;
using System;
using System.Text;

namespace GlyphEngine
{
    /// <summary>
    /// 通用渲染器
    /// </summary>
    internal class NRenderer : IRenderer, IFakable
    {
        /// <summary>
        /// 渲染缓存数组
        /// </summary>
        private CRendererBuffer[] buffers = { new CRendererBuffer(), new CRendererBuffer() };
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
        private CRendererBuffer renderer = new CRendererBuffer();
        /// <summary>
        /// 当前帧的渲染缓存
        /// </summary>
        private CRendererBuffer current => buffers[curIndex];
        /// <summary>
        /// 前一帧的渲染缓存
        /// </summary>
        private CRendererBuffer previous => buffers[preIndex];
        /// <summary>
        /// 
        /// </summary>
        private StringBuilder builder = new StringBuilder();

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
            if (dirty)// 有变化时重绘
            {
                Draw();
                Swap();
            }
        }

        /// <summary>
        /// 对比：找到当前帧真正需要被渲染的像素和需要被擦除的像素
        /// </summary>
        private bool Diff()
        {
            var dirty = false;

            // 被渲染的像素
            foreach (var p in current)
            {
                if (!previous.GetPixel(p.X, p.Y, out var q) || !p.Equals(q))
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
                    renderer.SetPixel(p.X, p.Y, CGlyph.Space);
                    dirty = true;
                }
            }

            return dirty;
        }

        /// <summary>
        /// 绘制
        /// </summary>
        private void Draw()
        {
            WritePixels();
            PrintPixels();

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
        /// 写入像素
        /// 使用 ANSI 转义，减少 Console API 的调用次数
        /// </summary>
        private void WritePixels()
        {
            foreach (var pixel in renderer)
            {
                builder.Append("\x1b[").Append(pixel.Y).Append(';').Append(pixel.X).Append('H')
                       .Append("\u001b[38;5;").Append((byte)pixel.Color).Append(";48;5;").Append((byte)pixel.BackgroundColor).Append('m')
                       .Append(pixel.Glyph).Append("\u001b[0m");
            }
        }

        /// <summary>
        /// 绘制像素
        /// </summary>
        private void PrintPixels()
        {
            var text = builder.ToString();
            if (!Faked) Console.Write(text);
            builder.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Faked { get; private set; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="faked"></param>
        public void Fake(bool faked)
        {
            Faked = faked;
        }
    }
}
