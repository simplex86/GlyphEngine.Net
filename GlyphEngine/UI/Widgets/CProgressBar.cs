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
    /// 进度条方向
    /// </summary>
    internal enum EProgressBarDirection
    {
        /// <summary>
        /// 从左往右
        /// </summary>
        Left = 0,
        /// <summary>
        /// 从下往上
        /// </summary>
        Up = 0,
        /// <summary>
        /// 从右往左
        /// </summary>
        Right = 1,
        /// <summary>
        /// 从上往下
        /// </summary>
        Down = 1,
    }

    /// <summary>
    /// 进度条
    /// </summary>
    public class CProgressBar : CWidget
    {
        private IProgressBar bar;

        /// <summary>
        /// 进度
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
        internal CProgressBar(int length, CVector2 localpostion, float amount, EProgressBarStyle style, EProgressBarDirection direction)
            : base(localpostion)
        {
            if (style == EProgressBarStyle.Horizontal)
            {
                bar = new CHProgressBar(length, amount, direction, GameObject);
            }
            else // if (style == EProgressBarStyle.Vertical)
            {
                 bar = new CVProgressBar(length, amount, direction, GameObject);
            }
        }
    }
}
