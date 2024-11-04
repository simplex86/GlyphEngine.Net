namespace SimpleX.CEngine.UI
{
    public class CPanelView : CRenderableObject
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int width { get; private set; } = 50;
        /// <summary>
        /// 高度
        /// </summary>
        public int height { get; private set; } = 20;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        protected CPanelView(int w, int h)
            : base(ERenderLayer.UI)
        {
            width = w;
            height = h;

            for (int n = 0; n < w * h; n++)
            {
                int c = n % w;
                int r = n / w;

                var pixel = CPixelPool.Instance.Alloc(c - w / 2, r - h / 2);
                pixel.symbol = GetPixel(c, r);
                AddPixel(pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private string GetPixel(int c, int r)
        {
            if (r == 0)
            {
                if (c == 0) return "╔";
                if (c == width - 1) return "╗";
                return "═";
            }
            if (r == height - 1)
            {
                if (c == 0) return "╚";
                if (c == width - 1) return "╝";
                return "═";
            }

            if (c == 0 ||
                c == width - 1)
            {
                return "║";
            }

            return " ";
        }
    }
}
