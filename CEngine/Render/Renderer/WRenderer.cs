using System;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace CEngine
{
    /// <summary>
    /// Windows系统专用渲染器
    /// </summary>
    internal class WRenderer : IRenderer
    {
        private WindowsNativeAPI.CharInfo[] buffer;
        private SafeFileHandle handle;

        /// <summary>
        /// 
        /// </summary>
        public WRenderer()
        {
            handle = WindowsNativeAPI.CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

            if (!handle.IsInvalid)
            {
                buffer = new WindowsNativeAPI.CharInfo[CWorld.width * CWorld.height];
            }
        }

        /// <summary>
        /// 往当前帧渲染缓存写入像素数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, char c, ConsoleColor color, ConsoleColor backgroundColor)
        {
            var i = y * CWorld.width + x;
            if (c == 0) backgroundColor = Console.BackgroundColor;

            buffer[i].Attributes = (short)((int)color | ((int)backgroundColor << 4));
            buffer[i].UnicodeChar = c;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        public void Render()
        {
            var rect = new WindowsNativeAPI.SmallRect() { 
                Left   = 0, 
                Top    = 0, 
                Right  = (short)CWorld.width, 
                Bottom = (short)CWorld.height 
            };

            WindowsNativeAPI.WriteConsoleOutputW(handle, 
                                                 buffer,
                                                 new WindowsNativeAPI.Coord() { X = (short)CWorld.width, Y = (short)CWorld.height },
                                                 new WindowsNativeAPI.Coord() { X = 0, Y = 0 }, 
                                                 ref rect);

            ResetBuffer();
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        private void ResetBuffer()
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i].Attributes = (short)((int)Console.ForegroundColor | ((int)Console.BackgroundColor << 4));
                buffer[i].UnicodeChar = ' ';
            }
        }
    }
}
