namespace GlyphEngine
{
    /// <summary>
    /// 进度条类型
    /// </summary>
    internal enum EProgressBarStyle
    {
        /// <summary>
        /// 水平
        /// </summary>
        Horizontal,
        /// <summary>
        /// 垂直
        /// </summary>
        Vertical,
    }

    /// <summary>
    /// 进度条
    /// </summary>
    public class CProgressBar : CWidget
    {
        private IProgressBar bar;

        /// <summary>
        /// 
        /// </summary>
        public float Amount
        {
            set { bar.Amount = value; }
            get { return bar.Amount; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="localpostion"></param>
        internal CProgressBar(int length, CVector2 localpostion, EProgressBarStyle style)
            : base(localpostion)
        {
            if (style == EProgressBarStyle.Horizontal)
            {
                bar = new CHProgressBar(length, GameObject);
            }
            else // if (style == EProgressBarStyle.Vertical)
            {
                 bar = new CVProgressBar(length, GameObject);
            }
        }
    }
}
