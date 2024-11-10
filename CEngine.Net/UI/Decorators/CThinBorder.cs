namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    internal class CThinBorder : IDecorator
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CPixel> pixels { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public CThinBorder(IView view)
        {
            pixels = new List<CPixel>(view.width * view.height);
            for (int y = 0; y < view.height; y++)
            {
                for (int x = 0; x < view.width; x++)
                {
                    var c = GetChar(view.width, view.height, x, y);
                    if (c != '\0')
                    {
                        pixels.Add(CPixelPool.Instance.Alloc(x - view.width / 2, y - view.height / 2, c));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static char GetChar(int w, int h, int x, int y)
        {
            if (y == 0)
            {
                if (x == 0) return '┌';
                if (x == w - 1) return '┐';
                return '─';
            }
            if (y == h - 1)
            {
                if (x == 0) return '└';
                if (x == w - 1) return '┘';
                return '─';
            }

            if (x == 0 ||
                x == w - 1)
            {
                return '│';
            }

            return '\0';
        }
    }
}
