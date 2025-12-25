using System;

namespace CEngine
{
    /// <summary>
    /// 子控件接口
    /// </summary>
    public interface IWidget : IView
    {
        /// <summary>
        /// 颜色
        /// </summary>
        ConsoleColor color { get; set; }
    }

    /// <summary>
    /// 子控件基类
    /// </summary>
    public class CWidget : CRenderableObject, IWidget
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; protected set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public ConsoleColor color
        {
            set
            {
                if (_color != value)
                {
                    _color = value;
                    Foreach(p => p.Color = _color);
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
        protected CWidget()
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
                pixel.Color = _color;
                AddPixel(pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal override void OnDestroy()
        {
            ClearPixels();
        }
    }
}
