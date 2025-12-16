using CEngine.Input;

namespace CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class CPanelView : CRenderableObject, IView
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int width { get; } = CWorld.width;
        /// <summary>
        /// 高度
        /// </summary>
        public int height { get; } = CWorld.height;

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
        internal CPanelView()
            : this(CWorld.width, CWorld.height)
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
            width = w;
            height = h;

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
                interaction.interactable)
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
            if (CKeyboard.Poll(out var evt))
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
            var idx = children.IndexOf(focus as CWidget);
            if (idx < 0) return;

            for (int i = idx + 1; i < children.Count; i++)
            {
                var child = children[i];
                if (child is IInteractable interaction &&
                    interaction.interactable)
                {
                    Focus(interaction);
                    return;
                }
            }

            for (int i = 0; i < idx; i++)
            {
                var child = children[i];
                if (child is IInteractable interaction &&
                    interaction.interactable)
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
            var idx = children.IndexOf(focus as CWidget);
            if (idx < 0) return;

            for (int i=idx - 1; i >= 0; i--)
            {
                var child = children[i];
                if (child is IInteractable interaction &&
                    interaction.interactable)
                {
                    Focus(interaction);
                    return;
                }
            }

            for (int i = children.Count - 1; i > idx; i--)
            {
                var child = children[i];
                if (child is IInteractable interaction &&
                    interaction.interactable)
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
                evt.keycode == (int)focus.keycode)
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
