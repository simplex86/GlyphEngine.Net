using System;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IProgressBarModifier
    {
        /// <summary>
        /// 
        /// </summary>
        ConsoleColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressbar"></param>
        void Fill(IProgressBar progressbar);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressbar"></param>
        void Modify(IProgressBar progressbar);
    }
}
