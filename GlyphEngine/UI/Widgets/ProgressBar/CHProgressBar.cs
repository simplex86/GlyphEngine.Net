using System;

namespace GlyphEngine
{
    /// <summary>
    /// 进度条基类
    /// </summary>
    internal class CHProgressBar : IProgressBar
    {
        /// <summary>
        /// 
        /// </summary>
        public int Length => length;
        /// <summary>
        /// 进度
        /// </summary>
        public float Amount
        {
            set
            {
                amount = Math.Clamp(value, 0.0f, 1.0f);
                Modify();
            }
            get { return amount; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal CRenderableObject Target => target;

        private int length = 0;
        private float amount = 0.0f;
        private EProgressBarDirection direction;
        private CRenderableObject target;
        private IProgressBarModifier modifier;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="localpostion"></param>
        internal CHProgressBar(int length, float amount, EProgressBarDirection direction, CRenderableObject target)
        {
            this.length = length;
            this.target = target;
            this.amount = amount;
            this.direction = direction;

            if (direction == EProgressBarDirection.Left)
                modifier = new CProgressBarLModifier();
            else if (direction == EProgressBarDirection.Right)
                modifier = new CProgressBarRModifier();

            Fill();
            Modify();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            modifier.Fill(this);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Modify()
        {
            modifier.Modify(this);
        }
    }
}
