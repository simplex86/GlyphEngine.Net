using System;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CProgressBarLModifier : IProgressBarModifier
    {
        /// <summary>
        /// 
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
        /// <param name="color"></param>
        public CProgressBarLModifier(ConsoleColor color)
        {
            this.Color = color;
        }

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
                var pixel = new CPixel(x + o, 0, glyphs[0], Color);
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
                target.Set(i, glyphs[8], Color);
            }
            if (ipart < progressbar.Length)
            {
                // 小数部分
                var index = (int)(fpart * glyphs.Length);
                target.Set(ipart, glyphs[index], Color);
                // 剩余部分
                for (int i = ipart + 1; i < progressbar.Length; i++)
                {
                    target.Set(i, glyphs[0], Color);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class CProgressBarRModifier : IProgressBarModifier
    {
        /// <summary>
        /// 
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static char[] glyphs = new char[] {
            ' ', // 0
            '▕',// 1
            '▐', // 2
            '█',// 3
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public CProgressBarRModifier(ConsoleColor color)
        {
            this.Color = color;
        }

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
                var pixel = new CPixel(x + o, 0, glyphs[0], Color);
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
                target.Set(progressbar.Length - i - 1, glyphs[3], Color);
            }
            if (ipart < progressbar.Length)
            {
                // 小数部分
                var index = (int)(fpart * glyphs.Length);
                target.Set(progressbar.Length - ipart - 1, glyphs[index], Color);
                // 剩余部分
                for (int i = ipart + 1; i < progressbar.Length; i++)
                {
                    target.Set(progressbar.Length - i - 1, glyphs[0], Color);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class CProgressBarUModifier : IProgressBarModifier
    {
        /// <summary>
        /// 
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static char[] glyphs = new char[] {
            ' ','▁','▂','▃','▄','▅','▆','▇','█',
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public CProgressBarUModifier(ConsoleColor color)
        {
            this.Color = color;
        }

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
                var pixel = new CPixel(0, o - y, glyphs[0], Color);
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
                target.Set(i, glyphs[8], Color);
            }
            if (ipart < progressbar.Length)
            {
                // 小数部分
                var index = (int)(fpart * glyphs.Length);
                target.Set(ipart, glyphs[index], Color);
                // 剩余部分
                for (int i = ipart + 1; i < progressbar.Length; i++)
                {
                    target.Set(i, glyphs[0], Color);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class CProgressBarDModifier : IProgressBarModifier
    {
        /// <summary>
        /// 
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static char[] glyphs = new char[] {
            ' ','▔','▀','█',
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public CProgressBarDModifier(ConsoleColor color)
        {
            this.Color = color;
        }

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
                var pixel = new CPixel(0, o - y, glyphs[0], Color);
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
                target.Set(progressbar.Length - i - 1, glyphs[3], Color);
            }
            if (ipart < progressbar.Length)
            {
                // 小数部分
                var index = (int)(fpart * glyphs.Length);
                target.Set(progressbar.Length - ipart - 1, glyphs[index], Color);
                // 剩余部分
                for (int i = ipart + 1; i < progressbar.Length; i++)
                {
                    target.Set(progressbar.Length - i - 1, glyphs[0], Color);
                }
            }
        }
    }
}
