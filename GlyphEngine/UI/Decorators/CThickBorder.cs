namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CThickBorder : CBorder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public CThickBorder(IView view)
            : base(view)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected override char Get(int w, int h, int x, int y)
        {
            if (y == 0)
            {
                if (x == 0) return '┏';
                if (x == w - 1) return '┓';
                return '━';
            }
            if (y == h - 1)
            {
                if (x == 0) return '┗';
                if (x == w - 1) return '┛';
                return '━';
            }

            if (x == 0 || x == w - 1)
            {
                return '┃';
            }

            return CChar.Empty;
        }
    }
}
