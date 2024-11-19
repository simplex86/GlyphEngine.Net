namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class CUIComponent : CRenderableObject, IView
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int width { get; protected set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int height { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public ConsoleColor color
        {
            set
            {
                if (_color != value)
                {
                    _color = value;
                    Foreach(p => p.color = _color);
                }
            }
            get { return _color; }
        }

        /// <summary>
        /// 
        /// </summary>
        private ConsoleColor _color = ConsoleColor.White;

        /// <summary>
        /// 
        /// </summary>
        protected CUIComponent()
            : base(ERenderLayer.UI)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decorator"></param>
        /// <param name="color"></param>
        protected void Apply(IDecorator decorator)
        {
            foreach (var pixel in decorator.pixels)
            {
                pixel.color = _color;
                AddPixel(pixel);
            }
        }
    }
}
