using System.Collections.Generic;

namespace CEngine
{
    internal class CBox : IDecorator
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CPixel> pixels { get; }

        public CBox(IView view)
        {
            pixels = new List<CPixel>(view.Width * view.Height);
            for (int y = 0; y < view.Height; y++)
            {
                for (int x = 0; x < view.Width; x++)
                {
                    pixels.Add(CPixelPool.Instance.Alloc(x - view.Width / 2, y - view.Height / 2, CChar.Space));
                }
            }
        }
    }
}
