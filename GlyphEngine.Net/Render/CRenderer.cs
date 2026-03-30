using System;
using System.Reflection;

namespace GlyphEngine
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
            var platform = CPlatformHelper.GetPlatform();
            // 查找自定义渲染器
            var types = CReflectionHelper.FindAll<IRenderer, CRendererEntryAttribute>();
            foreach (var type in types)
            {
                if (platform == GetPlatform(type))
                {
                    renderer = Activator.CreateInstance(type) as IRenderer;
                    return;
                }
            }
            // 如果没有自定义渲染器，则使用内置渲染器
            renderer = (platform == EPlatform.Windows) ? new NRenderer()
                                                       : new NRenderer();
        }

        /// <summary>
        /// 往当前帧渲染缓存写入像素数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="glyph"></param>
        /// <param name="color"></param>
        /// <param name="backgroundColor"></param>
        public void SetPixel(int x, int y, char glyph, CColor color, CColor backgroundColor)
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

        private EPlatform GetPlatform(Type type)
        {
            return type.GetCustomAttribute<CRendererEntryAttribute>().Platform;
        }
    }
}
