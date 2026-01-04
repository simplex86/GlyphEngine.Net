using System;

namespace GlyphEngine
{
    /// <summary>
    /// 可渲染接口
    /// </summary>
    internal interface IRenderable
    {
        /// <summary>
        /// 渲染层
        /// </summary>
        ulong Layer { get; }
        /// <summary>
        /// 可见性
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// 遍历像素
        /// </summary>
        /// <param name="action"></param>
        void Foreach(Action<CPixel> action);
    }
}
