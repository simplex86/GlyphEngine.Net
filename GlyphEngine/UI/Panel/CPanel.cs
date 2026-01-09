using System;
using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CPanel : IView, IContainable<CWidget>
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; internal set; } = CScreen.Width;
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; internal set; } = CScreen.Height;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get { return GameObject.Name; }
            set { GameObject.Name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public CTransform Transform => Destroyed ? null : GameObject.Transform;
        /// <summary>
        /// 子节点数量
        /// </summary>
        public int Count => Destroyed ? 0 : GameObject.Count;
        /// <summary>
        /// 
        /// </summary>
        public bool Destroyed
        {
            get
            {
                if (!CheckView()) return true;
                return destroyed;
            }
        }

        /// <summary>
        /// 视图
        /// </summary>
        private CRenderableObject GameObject = new CRenderableObject(ERenderLayer.UI);
        /// <summary>
        /// 当前获得焦点的组件
        /// </summary>
        private IInteractable focus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<CGameObject, CWidget> widgets = new Dictionary<CGameObject, CWidget>();
        /// <summary>
        /// 
        /// </summary>
        private bool destroyed = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="name"></param>
        protected internal CPanel()
        {
            Apply(EBorderStyle.Borderless);
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Start()
        {
            OnOpen();
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void OnOpen();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weidget"></param>
        public void Add(CWidget widget)
        {
            if (CheckView())
            {
                widget.SetParent(GameObject);
                widgets.TryAdd(widget.GameObject, widget);

                if (widget is IInteractable interaction &&
                    interaction.Interactabled)
                {
                    Focus(interaction);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CWidget GetChild(int index)
        {
            if (CheckView())
            {
                var gameobject = GameObject.GetChild(index);
                if (widgets.TryGetValue(gameobject, out var widget))
                {
                    return widget;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widget"></param>
        public void Remove(CWidget widget)
        {
            if (CheckView())
            {
                GameObject.Remove(widget.GameObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get<T>(string name) where T : CWidget
        {
            if (CheckView())
            {
                foreach (var widget in widgets.Values)
                {
                    if (widget.Name == name)
                    {
                        return widget as T;
                    }
                }
            }

            return null;
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
        /// 销毁
        /// </summary>
        internal void Destroy()
        {
            destroyed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void DestroyImmediately()
        {
            OnClose();
            // 销毁子节点
            foreach (var widget in widgets.Values)
            {
                widget.Destroy();
            }
            widgets.Clear();
            // 销毁视图
            GameObject.Destroy();
            GameObject = null;
        }

        protected abstract void OnClose();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckView()
        {
            if (GameObject == null || GameObject.Destroyed)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        private void Focus(IInteractable interaction)
        {
            focus?.LoseFocus();
            focus = interaction;
            focus?.OnFocus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        private void Apply(EBorderStyle style)
        {
            switch (style)
            {
                case EBorderStyle.ThinBorder:
                    Apply(new CThinBorder(this));
                    break;
                case EBorderStyle.ThickBorder:
                    Apply(new CThickBorder(this));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decorator"></param>
        private void Apply(IDecorator decorator)
        {
            foreach (var pixel in decorator.pixels)
            {
                GameObject.AddPixel(pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        internal void Update(float dt)
        {
            if (CInput.Poll(out var evt))
            {
                switch (evt.keycode)
                {
                    case (int)ConsoleKey.DownArrow:
                    case (int)ConsoleKey.RightArrow:
                    case (int)ConsoleKey.Tab:
                        if (evt.type == EKeyboardEventType.Up)
                        {
                            FocusNext();
                        }
                        break;
                    case (int)ConsoleKey.UpArrow:
                    case (int)ConsoleKey.LeftArrow:
                        if (evt.type == EKeyboardEventType.Up)
                        {
                            FocusPrev();
                        }
                        break;
                    default:
                        TryEnter(evt);
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void FocusNext()
        {
            //var idx = Children.IndexOf(focus as CWidget);
            //if (idx < 0) return;

            //for (int i = idx + 1; i < Children.Count; i++)
            //{
            //    var child = Children[i];
            //    if (child is IInteractable interaction &&
            //        interaction.Interactabled)
            //    {
            //        Focus(interaction);
            //        return;
            //    }
            //}

            //for (int i = 0; i < idx; i++)
            //{
            //    var child = Children[i];
            //    if (child is IInteractable interaction &&
            //        interaction.Interactabled)
            //    {
            //        Focus(interaction);
            //        return;
            //    }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        private void FocusPrev()
        {
            //    var idx = Children.IndexOf(focus as CWidget);
            //    if (idx < 0) return;

            //    for (int i=idx - 1; i >= 0; i--)
            //    {
            //        var child = Children[i];
            //        if (child is IInteractable interaction &&
            //            interaction.Interactabled)
            //        {
            //            Focus(interaction);
            //            return;
            //        }
            //    }

            //    for (int i = Children.Count - 1; i > idx; i--)
            //    {
            //        var child = Children[i];
            //        if (child is IInteractable interaction &&
            //            interaction.Interactabled)
            //        {
            //            Focus(interaction);
            //            return;
            //        }
            //    }
        }

        /// <summary>
        /// 
        /// </summary>
        private void TryEnter(CKeyboardEvent evt)
        {
            if (focus == null)
            {
                return;
            }

            if (evt.type == EKeyboardEventType.Up &&
                evt.keycode == (int)focus.Keycode)
            {
                focus.OnEnter();
            }
        }
    }
}
