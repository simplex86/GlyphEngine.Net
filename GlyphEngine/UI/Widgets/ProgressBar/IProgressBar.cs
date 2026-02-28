using System;

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
        ConsoleColor Color { get; set; }
    }
}
