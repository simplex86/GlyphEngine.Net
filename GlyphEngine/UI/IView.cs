namespace GlyphEngine
{
    /// <summary>
    /// UI视图
    /// </summary>
    public interface IView : ITransformable
    {
        /// <summary>
        /// 宽度
        /// </summary>
        int Width { get; }
        /// <summary>
        /// 高度
        /// </summary>
        int Height { get; }
    }
}
