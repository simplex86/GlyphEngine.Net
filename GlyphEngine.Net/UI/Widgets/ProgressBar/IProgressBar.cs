namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IProgressBar
    {
        /// <summary>
        /// 
        /// </summary>
        int Length { get; }
        /// <summary>
        /// 
        /// </summary>
        float Amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        CColor Color { get; set; }
    }
}
