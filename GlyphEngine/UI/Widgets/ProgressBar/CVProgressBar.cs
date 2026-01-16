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
        public float Amount
        {
            set
            {
                amount = Math.Clamp(value, 0.0f, 1.0f);
                Modify();
            }
            get { return amount; }
        }

        private float length = 0.0f;
        private float amount = 0.0f;
        private CRenderableObject target;

        private static char[] glyphs = new char[] { 
            ' ','▁','▂','▃','▄','▅','▆','▇','█',
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="localpostion"></param>
        internal CVProgressBar(int length, CRenderableObject target)
        {
            this.length = length;
            this.target = target;
            this.amount = 0.0f;

            Modify();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Modify()
        {
            var value = amount * length;
            var ipart = (int)value;
            var fpart = value - ipart;

            target.ClearPixels();

            // 整数部分
            for (int y=0; y<ipart; y++)
            {
                var pixel = CPixelPool.Instance.Alloc(0, y, glyphs[8]);
                target.AddPixel(pixel);
            }
            // 小数部分
            {
                var index = (int)(fpart * glyphs.Length);
                var pixel = CPixelPool.Instance.Alloc(0, ipart, glyphs[index]);
                target.AddPixel(pixel);
            }
            // 剩余部分
            {
                for (int y=ipart+1; y<length; y++)
                {
                    var pixel = CPixelPool.Instance.Alloc(0, y, glyphs[0]);
                    target.AddPixel(pixel);
                }
            }
        }
    }
}
