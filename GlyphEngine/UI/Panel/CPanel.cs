using System;
using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CPanel : IView, IContainable<CWidget>, IGameObjectOwner
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
        public CTransform Transform => CheckView() ? GameObject.Transform : null;
        /// <summary>
        /// 子节点数量
        /// </summary>
        public int Count => CheckView() ? GameObject.Count : 0;
        /// <summary>
        /// 是否已被销毁
        /// </summary>
        public bool Destroyed => CheckView() ? destroyed : true;

        /// <summary>
        /// 视图
        /// </summary>
        private CRenderableObject GameObject;
        /// <summary>
        /// 当前获得焦点的组件
        /// </summary>
        private IInteractable focus = null;
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
        internal protected CPanel()
        {
            GameObject = new CRenderableObject(ERenderLayer.UI, this);
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
                var child = GameObject.GetChild(index);
                return child.Owner as CWidget;
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
                for (int i = 0; i < Count; i++)
                {
                    var widget = GetChild(i);
                    if (widget.Name == name) return widget as T;
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
                case EBorderStyle.Thin:
                    Apply(new CBorder(this));
                    break;
                case EBorderStyle.Thick:
                    Apply(new CThickBorder(this));
                    break;
                case EBorderStyle.Round:
                    Apply(new CRoundBorder(this));
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

            OnUpdate(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        protected virtual void OnUpdate(float dt)
        {

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
