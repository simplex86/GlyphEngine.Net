using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal abstract class CBorder : IDecorator
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CPixel> pixels { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public CBorder(IView view)
        {
            Fill(view);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        protected virtual void Fill(IView view)
        {
            pixels = new List<CPixel>(view.Width * 2 + view.Height * 2 - 4);

            char c = (char)0;
            // 水平
            {
                int y = 0;
                for (int x = 0; x < view.Width; x++)
                {
                    y = 0;
                    c = Get(view.Width, view.Height, x, y);
                    if (c != CGlyph.Empty)
                    {
                        pixels.Add(CPixelPool.Instance.Alloc(x - view.Width / 2, y - view.Height / 2, c));
                    }

                    y = view.Height - 1;
                    c = Get(view.Width, view.Height, x, y);
                    if (c != CGlyph.Empty)
                    {
                        pixels.Add(CPixelPool.Instance.Alloc(x - view.Width / 2, y - view.Height / 2, c));
                    }
                }
            }
            // 垂直
            {
                int x = 0;
                for (int y = 1; y < view.Height - 1; y++)
                {
                    x = 0;
                    c = Get(view.Width, view.Height, x, y);
                    if (c != CGlyph.Empty)
                    {
                        pixels.Add(CPixelPool.Instance.Alloc(x - view.Width / 2, y - view.Height / 2, c));
                    }

                    x = view.Width - 1;
                    c = Get(view.Width, view.Height, x, y);
                    if (c != CGlyph.Empty)
                    {
                        pixels.Add(CPixelPool.Instance.Alloc(x - view.Width / 2, y - view.Height / 2, c));
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
        protected abstract char Get(int w, int h, int x, int y);
    }
}
