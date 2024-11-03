namespace SimpleX.CEngine
{
    /// <summary>
    /// 渲染层
    /// </summary>
    public enum ERenderLayer : ulong
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 1,
        /// <summary>
        /// UI
        /// </summary>
        UI = 1 << 1,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ERenderMask : ulong
    {
        /// <summary>
        /// 
        /// </summary>
        Nothing = 0,
        /// <summary>
        /// 
        /// </summary>
        Everything = 0xffffffff,
        /// <summary>
        /// 
        /// </summary>
        Default = 1,
        /// <summary>
        /// 
        /// </summary>
        UI = 2,
    }
}
