namespace GlyphEngine
{
    /// <summary>
    /// 辅助测试的接口
    /// 用于不便于开启控制台渲染时（例如Benchmark时）
    /// </summary>
    public interface IFakable
    {
        /// <summary>
        /// 
        /// </summary>
        bool Faked { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fake"></param>
        void Fake(bool fake);
    }
}
