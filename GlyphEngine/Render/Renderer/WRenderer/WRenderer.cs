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
        /// 
        /// </summary>
        private bool dirty = false;
        /// <summary>
        /// 写入缓存的大小
        /// </summary>
        private readonly static WindowsNativeAPI.Coord BUFFER_SIZE = new WindowsNativeAPI.Coord() { 
            X = (short)CScreen.Width, 
            Y = (short)CScreen.Height 
        };
        /// <summary>
        /// 写入缓存的位置
        /// </summary>
        private readonly static WindowsNativeAPI.Coord BUFFER_COOD = new WindowsNativeAPI.Coord() { 
            X = 0, 
            Y = 0
        };

        /// <summary>
        /// 
        /// </summary>
        public WRenderer()
        {
            handle = WindowsNativeAPI.CreateFile("CONOUT$", 
                                                 (uint)EFileAccessMode.Write, 
                                                 (uint)EFileShareMode.Write, 
                                                 IntPtr.Zero, 
                                                 FileMode.Open, 
                                                 (int)FileAttributes.None, 
                                                 IntPtr.Zero);

            if (!handle.IsInvalid)
            {
                buffer = new WindowsNativeAPI.CharInfo[CScreen.Width * CScreen.Height];
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

            var index = y * CScreen.Width + x;
            var attrs = (short)((int)color | ((int)backgroundColor << 4));

            if (buffer[index].Attributes != attrs ||
                buffer[index].UnicodeChar != glyph)
            {
                dirty = true;
                buffer[index].Attributes = attrs;
                buffer[index].UnicodeChar = glyph;
            }
        }

        /// <summary>
        /// 渲染
        /// </summary>
        public void Render()
        {
            if (dirty)
            {
                PrevProcess();
                WindowsNativeAPI.WriteConsoleOutputW(handle, buffer, BUFFER_SIZE, BUFFER_COOD, ref region);
                PostProcess();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrevProcess()
        {

        }

        /// <summary>
        /// 重置写入缓存
        /// </summary>
        private void PostProcess()
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i].Attributes = (short)((int)Console.ForegroundColor | ((int)Console.BackgroundColor << 4));
                buffer[i].UnicodeChar = CGlyph.Space;
            }

            region.Left   = 0;
            region.Top    = 0;
            region.Right  = (short)CScreen.Width;
            region.Bottom = (short)CScreen.Height;

            dirty = false;
        }
    }
}
