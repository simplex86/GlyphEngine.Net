namespace SimpleX.CEngine
{
    /// <summary>
    /// 
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
        internal List<char> chars = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transparent"></param>
        public CTexture()
            : this(true)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public CTexture(bool transparent)
        {
            this.transparent = transparent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public CTexture(string filepath)
            : this()
        {
            Load(filepath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="transparent"></param>
        public CTexture(string filepath, bool transparent)
            : this(transparent)
        {
            Load(filepath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public void Load(string filepath)
        {
            this.LoadTexture(filepath, transparent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CTexture FromFile(string filepath, bool transparent)
        {
            return new CTexture(filepath, transparent);
        }
    }
}
