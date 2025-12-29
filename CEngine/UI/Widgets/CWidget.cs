using System;
using System.Collections.Generic;

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
        ConsoleColor Color { get; set; }
    }

    /// <summary>
    /// 子控件基类
    /// </summary>
    public class CWidget : IWidget, IContainable<CWidget>
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
            get { return view.Name; }
            internal set { view.Name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public CGameObject GameObject => view;
        /// <summary>
        /// 
        /// </summary>
        public CTransform Transform => view.Transform;
        /// <summary>
        /// 
        /// </summary>
        public int Count => view.Count;

        /// <summary>
        /// 
        /// </summary>
        internal CRenderableObject view = new CRenderableObject(ERenderLayer.UI);

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<CGameObject, CWidget> widgets = new Dictionary<CGameObject, CWidget>();

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
                    view.Foreach(p => p.Color = color);
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
        protected CWidget()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weidget"></param>
        public void Add(CWidget widget)
        {
            view.Add(widget.GameObject);
            widgets.TryAdd(widget.GameObject, widget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CWidget GetChild(int index)
        {
            var gameobject = view.GetChild(index);
            if (widgets.TryGetValue(gameobject, out var widget))
            {
                return widget;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widget"></param>
        public void Remove(CWidget widget)
        {
            view.Remove(widget.GameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get<T>(string name) where T : CWidget
        {
            foreach (var widget in widgets.Values)
            {
                if (widget.Name == name)
                {
                    return widget as T;
                }
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
                view.AddPixel(pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Destroy()
        {
            view.Destroy();
            view = null;

            OnDestroy();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnDestroy()
        {

        }
    }
}
