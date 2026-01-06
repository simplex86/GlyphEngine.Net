using System;
using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 纹理
    /// 以字符作像素
    /// 只读
    /// </summary>
    public sealed class CTexture
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; internal set; }

        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; internal set; }
        /// <summary>
        /// 是否透明
        /// </summary>
        public bool Transparent { get; } = true;

        /// <summary>
        /// 字符集
        /// </summary>
        internal List<char> Glyphs { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        internal int Refc { get; private set; } = 0;
        
        /// <summary>
        /// 
        /// </summary>
        internal CTexture(bool transparent)
        {
            this.Transparent = transparent;
        }

        /// <summary>
        /// 增加引用
        /// </summary>
        internal void Refrence()
        {
            Refc++;
        }

        /// <summary>
        /// 减少引用
        /// </summary>
        internal void Destroy()
        {
            Refc = Math.Max(0, Refc - 1);
        }
    }
}
