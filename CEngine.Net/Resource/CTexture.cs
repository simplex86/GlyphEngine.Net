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
        /// 字符集
        /// </summary>
        internal List<char> chars = null;

        /// <summary>
        /// 
        /// </summary>
        public CTexture()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public CTexture(string filepath)
        {
            Load(filepath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public void Load(string filepath)
        {
            this.LoadTexture(filepath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CTexture FromFile(string filepath)
        {
            return new CTexture(filepath);
        }
    }
}
