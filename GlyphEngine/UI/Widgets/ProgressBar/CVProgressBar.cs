using System;

namespace GlyphEngine
{
    /// <summary>
    /// 进度条基类
    /// </summary>
    internal class CVProgressBar : IProgressBar
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
        internal CVProgressBar(int length, float amount, EProgressBarDirection direction, CRenderableObject target)
        {
            this.length = length;
            this.target = target;
            this.amount = amount;
            this.direction = direction;

            if (direction == EProgressBarDirection.Up)
                modifier = new CProgressBarUModifier();
            else if (direction == EProgressBarDirection.Down)
                modifier = new CProgressBarDModifier();

            Fill();
            Modify();
        }

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
