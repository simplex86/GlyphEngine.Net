namespace SimpleX.CEngine.UI
{
    internal class CBox : IDecorator
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CPixel> pixels { get; }

        public CBox(IView view)
        {
            pixels = new List<CPixel>(view.width * view.height);
            for (int y = 0; y < view.height; y++)
            {
                for (int x = 0; x < view.width; x++)
                {
                    pixels.Add(CPixelPool.Instance.Alloc(x - view.width / 2, y - view.height / 2, ' '));
                }
            }
        }
    }
}
