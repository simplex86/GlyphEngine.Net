namespace SimpleX.CEngine
{
    /// <summary>
    /// 可换肤接口
    /// </summary>
    internal interface ISkinable
    {
        /// <summary>
        /// 加载皮肤
        /// </summary>
        void LoadSkins();

        /// <summary>
        /// 应用指定名字的皮肤
        /// </summary>
        /// <param name="skinName"></param>
        void ApplySkin(string skinName);
    }
}
