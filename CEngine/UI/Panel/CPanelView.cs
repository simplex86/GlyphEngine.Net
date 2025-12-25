using System;
using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CPanelView : CRenderableObject, IView
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
        /// 当前获得焦点的组件
        /// </summary>
        private IInteractable focus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, CWidget> widgets = new Dictionary<string, CWidget>();

        /// <summary>
        /// 
        /// </summary>
        public CPanelView()
            : this(CScreen.Width, CScreen.Height)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        internal CPanelView(int w, int h)
            : base(ERenderLayer.UI)
        {
            Width = w;
            Height = h;

            BuildBorder(EBorderStyle.Borderless);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        internal void AddComponent(CWidget component, string name, bool focused = false)
        {
            Add(component);
            widgets.TryAdd(name, component);

            if (focused &&
                component is IInteractable interaction &&
                interaction.Interactabled)
            {
                Focus(interaction);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        internal T GetComponent<T>(string name) where T : CWidget
        {
            if (widgets.TryGetValue(name, out var component))
            {
                return component as T;
            }
            return null;
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
        /// <param name="decorator"></param>
        private void Apply(IDecorator decorator)
        {
            foreach (var pixel in decorator.pixels)
            {
                AddPixel(pixel);
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
            var idx = Children.IndexOf(focus as CWidget);
            if (idx < 0) return;

            for (int i = idx + 1; i < Children.Count; i++)
            {
                var child = Children[i];
                if (child is IInteractable interaction &&
                    interaction.Interactabled)
                {
                    Focus(interaction);
                    return;
                }
            }

            for (int i = 0; i < idx; i++)
            {
                var child = Children[i];
                if (child is IInteractable interaction &&
                    interaction.Interactabled)
                {
                    Focus(interaction);
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void FocusPrev()
        {
            var idx = Children.IndexOf(focus as CWidget);
            if (idx < 0) return;

            for (int i=idx - 1; i >= 0; i--)
            {
                var child = Children[i];
                if (child is IInteractable interaction &&
                    interaction.Interactabled)
                {
                    Focus(interaction);
                    return;
                }
            }

            for (int i = Children.Count - 1; i > idx; i--)
            {
                var child = Children[i];
                if (child is IInteractable interaction &&
                    interaction.Interactabled)
                {
                    Focus(interaction);
                    return;
                }
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        private void BuildBorder(EBorderStyle style)
        {
            //var box = new CBox(this);
            //Apply(box);

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
    }
}
