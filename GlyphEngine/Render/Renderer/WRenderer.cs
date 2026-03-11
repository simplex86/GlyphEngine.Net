using System;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace GlyphEngine
{
    /// <summary>
    /// Windows系统专用渲染器
    /// </summary>
    internal class WRenderer : IRenderer
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
        /// 当前帧的渲染缓存
        /// </summary>
        private CRendererBuffer current => buffers[curIndex];
        /// <summary>
        /// 前一帧的渲染缓存
        /// </summary>
        private CRendererBuffer previous => buffers[preIndex];

        /// <summary>
        /// 字符缓存
        /// </summary>
        private WindowsNativeAPI.CharInfo[] buffer;
        /// <summary>
        /// 控制台句柄
        /// </summary>
        private SafeFileHandle handle;
        /// <summary>
        /// 写入区
        /// </summary>
        private WindowsNativeAPI.SmallRect region = new WindowsNativeAPI.SmallRect() { 
            Left   = 0, 
            Top    = 0, 
            Right  = (short)CScreen.Width, 
            Bottom = (short)CScreen.Height 
        };

        /// <summary>
        /// 渲染区域左上角X坐标
        /// </summary>
        private short minx = short.MaxValue;
        /// <summary>
        /// 渲染区域左上角Y坐标
        /// </summary>
        private short miny = short.MaxValue;
        /// <summary>
        /// 渲染区域右下角X坐标
        /// </summary>
        private short maxx = short.MinValue;
        /// <summary>
        /// 渲染区域右下角Y坐标
        /// </summary>
        private short maxy = short.MinValue;

        /// <summary>
        /// 写入缓存的位置
        /// </summary>
        private WindowsNativeAPI.Coord coord = new WindowsNativeAPI.Coord()
        {
            X = 0,
            Y = 0
        };

        /// <summary>
        /// 写入缓存的大小
        /// </summary>
        private readonly static WindowsNativeAPI.Coord BUFFER_SIZE = new WindowsNativeAPI.Coord() 
        { 
            X = (short)CScreen.Width, 
            Y = (short)CScreen.Height 
        };

        /// <summary>
        /// 
        /// </summary>
        public WRenderer()
            : this(CScreen.Width, CScreen.Height)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public WRenderer(int width, int height)
        {
            this.width = width;
            this.height = height;

            handle = WindowsNativeAPI.CreateFile("CONOUT$", 
                                                 (uint)EFileAccessMode.Write, 
                                                 (uint)EFileShareMode.Write, 
                                                 IntPtr.Zero, 
                                                 FileMode.Open, 
                                                 (int)FileAttributes.None, 
                                                 IntPtr.Zero);

            if (!handle.IsInvalid)
            {
                buffer = new WindowsNativeAPI.CharInfo[width * height];
            }
        }

        /// <summary>
        /// 往当前帧渲染缓存写入像素数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, char glyph, ConsoleColor color, ConsoleColor backgroundColor)
        {
            if (glyph == CGlyph.Empty)
            {
                backgroundColor = Console.BackgroundColor;
            }

            current.SetPixel(x, y, glyph, color, backgroundColor);
        }

        /// <summary>
        /// 渲染
        /// </summary>
        public void Render()
        {
            var dirty = Diff();
            if (dirty)// 有变化时重绘
            {
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

            // 被渲染的像素
            foreach (var p in current)
            {
                if (!previous.GetPixel(p.X, p.Y, out var q) || !p.Equals(q))
                {
                    WriteBuffer(p.X, p.Y, p.Glyph, p.Color, p.BackgroundColor);
                    dirty = true;
                }
            }
            // 被擦除的像素
            foreach (var p in previous)
            {
                if (!current.GetPixel(p.X, p.Y, out var q))
                {
                    WriteBuffer(p.X, p.Y, CGlyph.Space, Console.ForegroundColor, Console.BackgroundColor);
                    dirty = true;
                }
            }

            return dirty;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Draw()
        {
            try
            {
                PrevProcess();
                WindowsNativeAPI.WriteConsoleOutputW(handle, buffer, BUFFER_SIZE, coord, ref region);
                PostProcess();
            }
            catch (Exception ex)
            {
                CDebug.Error($"render error: {ex.Message}");
            }
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
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        /// <param name="backgroundColor"></param>
        private void WriteBuffer(int x, int y, char glyph, ConsoleColor color, ConsoleColor backgroundColor)
        {
            var index = y * width + x;
            var attrs = (short)((int)color | ((int)backgroundColor << 4));

            if (buffer[index].Attributes != attrs ||
                buffer[index].UnicodeChar != glyph)
            {
                buffer[index].Attributes = attrs;
                buffer[index].UnicodeChar = glyph;

                minx = (short)Math.Min(minx, x);
                miny = (short)Math.Min(miny, y);
                maxx = (short)Math.Max(maxx, x);
                maxy = (short)Math.Max(maxy, y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrevProcess()
        {
            // 设置渲染区域（Right 和 Bottom 是 exclusive 的）
            region.Left   = minx;
            region.Top    = miny;
            region.Right  = (short)(maxx + 1);
            region.Bottom = (short)(maxy + 1);
            // 计算缓冲区起始位置（相对于缓冲区的坐标）
            coord.X = minx;
            coord.Y = miny;
        }

        /// <summary>
        /// 重置写入缓存
        /// </summary>
        private void PostProcess()
        {
            minx = short.MaxValue;
            miny = short.MaxValue;
            maxx = short.MinValue;
            maxy = short.MinValue;
        }
    }
}
