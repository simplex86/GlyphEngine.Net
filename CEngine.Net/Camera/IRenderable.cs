namespace SimpleX.CEngine
{
    /// <summary>
    /// 可渲染接口
    /// </summary>
    internal interface IRenderable
    {
        /// <summary>
        /// 是否可渲染
        /// </summary>
        bool enabled { get; }

        /// <summary>
        /// 遍历像素
        /// </summary>
        /// <param name="action"></param>
        void Foreach(Action<CPixel> action);
    }
}
