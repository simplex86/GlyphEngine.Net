namespace GlyphEngine
{
    /// <summary>
    /// 可换肤接口
    /// </summary>
    internal interface ISkinable
    {
        /// <summary>
        /// 应用指定名字的皮肤
        /// </summary>
        /// <param name="skinname"></param>
        void ApplySkin(string skinname);
    }
}
