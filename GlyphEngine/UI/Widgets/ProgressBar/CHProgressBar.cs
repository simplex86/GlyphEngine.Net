using System;
using System.Reflection;

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
            ' ', // 0
            '▏',// 1
            '▎',// 2
            '▍',// 3
            '▌',// 4
            '▋',// 5
            '▊',// 6
            '▉',// 7
            '█',// 8
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="localpostion"></param>
        internal CHProgressBar(int length, CRenderableObject target)
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
            for (int x=0; x<ipart; x++)
            {
                var pixel = CPixelPool.Instance.Alloc(x, 0, glyphs[8]);
                target.AddPixel(pixel);
            }
            // 小数部分
            {
                var index = (int)(fpart * glyphs.Length);
                var pixel = CPixelPool.Instance.Alloc(ipart, 0, glyphs[index]);
                target.AddPixel(pixel);
            }
            // 剩余部分
            {
                for (int x=ipart+1; x<length; x++)
                {
                    var pixel = CPixelPool.Instance.Alloc(x, 0, glyphs[0]);
                    target.AddPixel(pixel);
                }
            }
        }
    }
}
