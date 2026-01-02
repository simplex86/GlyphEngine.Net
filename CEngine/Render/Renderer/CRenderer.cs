using System;
using System.Runtime.InteropServices;

namespace CEngine
{
    /// <summary>
    /// 渲染器
    /// </summary>
    internal class CRenderer
    {
        /// <summary>
        /// 
        /// </summary>
        private IRenderer renderer;

        /// <summary>
        /// 
        /// </summary>
        internal CRenderer()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                renderer = new NRenderer();
            }
            else
            {
                renderer = new NRenderer();
            }
        }

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
            renderer.SetPixel(x, y, glyph, color, backgroundColor);
        }

        /// <summary>
        /// 渲染
        /// </summary>
        public void Render()
        {
            renderer.Render();
        }
    }
}
