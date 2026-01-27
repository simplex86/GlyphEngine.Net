namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CProgressBarLModifier : IProgressBarModifier
    {
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
        /// <param name="progressbar"></param>
        public void Fill(IProgressBar progressbar)
        {
            var target = (progressbar as CHProgressBar).Target;

            var o = -progressbar.Length / 2;
            for (int x = 0; x < progressbar.Length; x++)
            {
                var pixel = CPixelPool.Instance.Alloc(x + o, 0, glyphs[0]);
                target.AddPixel(pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressbar"></param>
        public void Modify(IProgressBar progressbar)
        {
            var value = progressbar.Amount * progressbar.Length;
            var ipart = (int)value;
            var fpart = value - ipart;

            var target = (progressbar as CHProgressBar).Target;

            // 整数部分
            for (int i = 0; i < ipart; i++)
            {
                target.GetPixel(i).Glyph = glyphs[8];
            }
            if (ipart < progressbar.Length)
            {
                // 小数部分
                var index = (int)(fpart * glyphs.Length);
                target.GetPixel(ipart).Glyph = glyphs[index];
                // 剩余部分
                for (int i = ipart + 1; i < progressbar.Length; i++)
                {
                    target.GetPixel(i).Glyph = glyphs[0];
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class CProgressBarRModifier : IProgressBarModifier
    {
        private static char[] glyphs = new char[] {
            ' ', // 0
            '▕',// 1
            '▐', // 2
            '█',// 3
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressbar"></param>
        public void Fill(IProgressBar progressbar)
        {
            var target = (progressbar as CHProgressBar).Target;

            var o = -progressbar.Length / 2;
            for (int x = 0; x < progressbar.Length; x++)
            {
                var pixel = CPixelPool.Instance.Alloc(x + o, 0, glyphs[0]);
                target.AddPixel(pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressbar"></param>
        public void Modify(IProgressBar progressbar)
        {
            var value = progressbar.Amount * progressbar.Length;
            var ipart = (int)value;
            var fpart = value - ipart;

            var target = (progressbar as CHProgressBar).Target;

            // 整数部分
            for (int i = 0; i < ipart; i++)
            {
                target.GetPixel(progressbar.Length - i - 1).Glyph = glyphs[3];
            }
            if (ipart < progressbar.Length)
            {
                // 小数部分
                var index = (int)(fpart * glyphs.Length);
                target.GetPixel(progressbar.Length - ipart - 1).Glyph = glyphs[index];
                // 剩余部分
                for (int i = ipart + 1; i < progressbar.Length; i++)
                {
                    target.GetPixel(progressbar.Length - i - 1).Glyph = glyphs[0];
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class CProgressBarUModifier : IProgressBarModifier
    {
        private static char[] glyphs = new char[] {
            ' ','▁','▂','▃','▄','▅','▆','▇','█',
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressbar"></param>
        public void Fill(IProgressBar progressbar)
        {
            var target = (progressbar as CVProgressBar).Target;

            var o = progressbar.Length / 2;
            for (int y = 0; y < progressbar.Length; y++)
            {
                var pixel = CPixelPool.Instance.Alloc(0, o - y, glyphs[0]);
                target.AddPixel(pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressbar"></param>
        public void Modify(IProgressBar progressbar)
        {
            var value = progressbar.Amount * progressbar.Length;
            var ipart = (int)value;
            var fpart = value - ipart;

            var target = (progressbar as CVProgressBar).Target;

            // 整数部分
            for (int i = 0; i < ipart; i++)
            {
                target.GetPixel(i).Glyph = glyphs[8];
            }
            if (ipart < progressbar.Length)
            {
                // 小数部分
                var index = (int)(fpart * glyphs.Length);
                target.GetPixel(ipart).Glyph = glyphs[index];
                // 剩余部分
                for (int i = ipart + 1; i < progressbar.Length; i++)
                {
                    target.GetPixel(i).Glyph = glyphs[0];
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class CProgressBarDModifier : IProgressBarModifier
    {
        private static char[] glyphs = new char[] {
            ' ','▔','▀','█',
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressbar"></param>
        public void Fill(IProgressBar progressbar)
        {
            var target = (progressbar as CVProgressBar).Target;

            var o = progressbar.Length / 2;
            for (int y = 0; y < progressbar.Length; y++)
            {
                var pixel = CPixelPool.Instance.Alloc(0, o - y, glyphs[0]);
                target.AddPixel(pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressbar"></param>
        public void Modify(IProgressBar progressbar)
        {
            var value = progressbar.Amount * progressbar.Length;
            var ipart = (int)value;
            var fpart = value - ipart;

            var target = (progressbar as CVProgressBar).Target;

            // 整数部分
            for (int i = 0; i < ipart; i++)
            {
                target.GetPixel(progressbar.Length - i - 1).Glyph = glyphs[3];
            }
            if (ipart < progressbar.Length)
            {
                // 小数部分
                var index = (int)(fpart * glyphs.Length);
                target.GetPixel(progressbar.Length - ipart - 1).Glyph = glyphs[index];
                // 剩余部分
                for (int i = ipart + 1; i < progressbar.Length; i++)
                {
                    target.GetPixel(progressbar.Length - i - 1).Glyph = glyphs[0];
                }
            }
        }
    }
}
