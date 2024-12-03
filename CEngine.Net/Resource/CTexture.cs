namespace SimpleX.CEngine
{
    /// <summary>
    /// 纹理，以字符作像素，只读
    /// </summary>
    public sealed class CTexture
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int width { get; internal set; }

        /// <summary>
        /// 高
        /// </summary>
        public int height { get; internal set; }
        /// <summary>
        /// 是否透明
        /// </summary>
        public bool transparent { get; } = true;

        /// <summary>
        /// 字符集
        /// </summary>
        internal List<char> chars { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        internal int refc { get; set; } = 0;
        
        /// <summary>
        /// 
        /// </summary>
        internal CTexture(bool transparent)
        {
            this.transparent = transparent;
        }
    }
}
