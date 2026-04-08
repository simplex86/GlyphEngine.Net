using System;
using System.Runtime.InteropServices;

namespace GlyphEngine
{
    /// <summary>
    /// 渲染器接口
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// 往当前帧渲染缓存写入像素数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        /// <param name="backgroundColor"></param>
        void SetPixel(int x, int y, char glyph, in CColor color, in CColor backgroundColor);

        /// <summary>
        /// 渲染
        /// </summary>
        void Render();
    }

    /// <summary>
    /// 渲染器特性
    /// </summary>
    public class CRendererEntryAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public EPlatform Platform { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        public CRendererEntryAttribute(EPlatform platform)
        {
            Platform = platform;
        }
    }
}
