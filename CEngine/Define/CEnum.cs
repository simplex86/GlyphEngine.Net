namespace CEngine
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
        Everything = 0xFFFFFFFFFFFFFFFFuL,
        /// <summary>
        /// 
        /// </summary>
        Default = 1,
        /// <summary>
        /// 
        /// </summary>
        UI = 1 << 1,
    }

    /// <summary>
    /// 边框类型
    /// </summary>
    public enum EBorderStyle
    {
        /// <summary>
        /// 无边框
        /// </summary>
        Borderless = 0,
        /// <summary>
        /// 细边框
        /// </summary>
        ThinBorder = 1,
        /// <summary>
        /// 厚边框
        /// </summary>
        ThickBorder = 2,
    }
}
