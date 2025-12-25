using System;

namespace CEngine
{
    /// <summary>
    /// 渲染器接口
    /// </summary>
    internal interface IRenderer
    {
        /// <summary>
        /// 往当前帧渲染缓存写入像素数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        /// <param name="backgroundColor"></param>
        void SetPixel(int x, int y, char glyph, ConsoleColor color, ConsoleColor backgroundColor);

        /// <summary>
        /// 渲染
        /// </summary>
        void Render();
    }
}
