using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

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
        private static Dictionary<OSPlatform, EPlatform> platforms = new Dictionary<OSPlatform, EPlatform>()
        {
            { OSPlatform.Windows,   EPlatform.Windows },
            { OSPlatform.OSX,       EPlatform.Mac },
            { OSPlatform.Linux,     EPlatform.Linux },
        };

        /// <summary>
        /// 
        /// </summary>
        internal CRenderer()
        {
            var platform = EPlatform.Unknown;
            foreach (var kv in platforms)
            {
                if (RuntimeInformation.IsOSPlatform(kv.Key))
                {
                    platform = kv.Value;
                    break;
                }
            }
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
            renderer = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? new WRenderer()
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

        private EPlatform GetPlatform(Type type)
        {
            return type.GetCustomAttribute<CRendererEntryAttribute>().Platform;
        }
    }
}
