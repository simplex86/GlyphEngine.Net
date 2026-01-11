using System;
using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 子控件接口
    /// </summary>
    public interface IWidget : IView
    {
        /// <summary>
        /// 颜色
        /// </summary>
        ConsoleColor Color { get; set; }
    }

    /// <summary>
    /// 子控件基类
    /// </summary>
    public class CWidget : IWidget, IContainable<CWidget>, IGameObjectOwner
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
        /// 
        /// </summary>
        public string Name
        {
            get { return GameObject.Name; }
            internal set { GameObject.Name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public CTransform Transform => GameObject.Transform;
        /// <summary>
        /// 
        /// </summary>
        public int Count => GameObject.Count;

        /// <summary>
        /// 
        /// </summary>
        internal CRenderableObject GameObject { get; private set; } = null;

        /// <summary>
        /// 颜色
        /// </summary>
        public ConsoleColor Color
        {
            set
            {
                if (color != value)
                {
                    color = value;
                    GameObject.Foreach(p => p.Color = color);
                }
            }
            get { return color; }
        }

        /// <summary>
        /// 
        /// </summary>
        private ConsoleColor color = ConsoleColor.White;

        /// <summary>
        /// 
        /// </summary>
        protected CWidget(CVector2 localposition)
        {
            GameObject = new CRenderableObject(ERenderLayer.UI, this);
            Transform.LocalPosition = localposition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weidget"></param>
        public void Add(CWidget widget)
        {
            widget.SetParent(GameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CWidget GetChild(int index)
        {
            var child = GameObject.GetChild(index);
            return child.Owner as CWidget;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widget"></param>
        public void Remove(CWidget widget)
        {
            GameObject.Remove(widget.GameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get<T>(string name) where T : CWidget
        {
            for (int i = 0; i<Count; i++)
            {
                var widget = GetChild(i);
                if (widget.Name == name) return widget as T;
            }

            return null;
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
                pixel.Color = color;
                GameObject.AddPixel(pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        internal void SetParent(CGameObject parent)
        {
            GameObject.SetParent(parent);
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Destroy()
        {
            OnDestroy();
            // 销毁视图
            GameObject.Destroy();
            GameObject = null;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnDestroy()
        {

        }
    }
}
