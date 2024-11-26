namespace SimpleX.CEngine
{
    /// <summary>
    /// 皮肤
    /// </summary>
    internal class CSkin
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal List<CPixel> pixels { get; } = new List<CPixel>();

        public CSkin(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        internal void Set(int x, int y, char c, ConsoleColor color)
        {
            if (!Get(x, y, out var pixel))
            {
                pixel = CPixelPool.Instance.Alloc(x, y);
                pixels.Add(pixel);
            }

            pixel.c = c;
            pixel.color = color;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private bool Get(int x, int y, out CPixel pixel)
        {
            pixel = null;

            foreach (CPixel p in pixels)
            {
                if (p.x == x && p.y == y)
                {
                    pixel = p;
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        internal void Apply(IRenderable renderable)
        {
            renderable.Foreach(pixel =>
            {
                if (Get(pixel.x, pixel.y, out var p))
                {
                    pixel.c = p.c;
                    pixel.color = p.color;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Destroy()
        {
            CPixelPool.Instance.Release(pixels);
            pixels.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal CSkin Clone()
        {
            var clone = new CSkin(name);
            foreach (var pixel in pixels)
            {
                var clonepixel = pixel.Clone();
                clone.pixels.Add(clonepixel);
            }

            return clone;
        }
    }
}
