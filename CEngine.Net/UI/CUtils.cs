namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CUtils
    {
        internal static List<CPixel> GetBox(int w, int h)
        {
            var pixels = new List<CPixel>();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    pixels.Add(CPixelPool.Instance.Alloc(x - w / 2, y - h / 2, ' '));
                }
            }

            return pixels;
        }

        internal static List<CPixel> GetBorder(int w, int h, bool thin = false)
        {
            var pixels = new List<CPixel>();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    var c = thin ? GetThinChar(w, h, x, y) : GetChar(w, h, x, y);
                    if (c != '\0')
                    {
                        pixels.Add(CPixelPool.Instance.Alloc(x - w / 2, y - h / 2, c));
                    }
                }
            }

            return pixels;
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

            return '\0';
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static char GetThinChar(int w, int h, int x, int y)
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
