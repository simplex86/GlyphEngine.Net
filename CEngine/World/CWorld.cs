namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public static class CWorld
    {
        /// <summary>
        /// 
        /// </summary>
        public static int width => Console.BufferWidth;
        /// <summary>
        /// 
        /// </summary>
        public static int height => Console.BufferHeight;
        /// <summary>
        /// 世界坐标系中心
        /// </summary>
        internal static Vector2 center { get; } = new Vector2(width / 2, height / 2);
    }
}
