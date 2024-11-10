namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 按钮
    /// </summary>
    public class CButton : CUIComponent, IInteractable
    {
        /// <summary>
        /// 样式
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// 有边框
            /// </summary>
            Border = 1,
            /// <summary>
            /// 无边框
            /// </summary>
            Borderless = 2,
        }

        /// <summary>
        /// 是否可交互
        /// </summary>
        public bool interactable { get; set; } = true;
        /// <summary>
        /// 响应的按键
        /// </summary>
        public ConsoleKey keycode { get; } = ConsoleKey.Enter;
        /// <summary>
        /// 获得焦点时的颜色
        /// </summary>
        public ConsoleColor focusColor { get; set; } = ConsoleColor.Red;
        /// <summary>
        /// 失去焦点时的颜色
        /// </summary>
        public ConsoleColor unfocusColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// 样式
        /// </summary>
        private Style style = Style.Border;
        /// <summary>
        /// 点击事件列表
        /// </summary>
        private List<Action> onClicked = new List<Action>();
        /// <summary>
        /// 文本子组件
        /// </summary>
        private CText text = null;
        /// <summary>
        /// 
        /// </summary>
        private IDecorator box = null;
        /// <summary>
        /// 
        /// </summary>
        private IDecorator border = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="unfocusColor"></param>
        /// <param name="focusColor"></param>
        /// <param name="style"></param>
        public CButton(string text, Vector2 position, ConsoleKey keycode, ConsoleColor unfocusColor, ConsoleColor focusColor, Style style = Style.Border)
        {
            this.interactable = true;
            this.transform.position = position;
            this.keycode = keycode;
            this.unfocusColor = unfocusColor;
            this.focusColor = focusColor;
            this.style = style;

            this.text = new CText(text);
            AddChild(this.text);

            this.width  = this.text.width  + 2;
            this.height = this.text.height + 2;

            box = new CBox(this);
            Apply(box);

            if (style == Style.Border)
            {
                border = new CThinBorder(this);
                Apply(border);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void AddClick(Action action)
        {
            onClicked.Add(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void RemoveClick(Action action)
        {
            onClicked.Remove(action);
        }

        /// <summary>
        /// 获得焦点
        /// </summary>
        public void OnFocus()
        {
            if (style == Style.Border)
            {
                this.color = focusColor;
                Apply(border);

                text.color = color;
            }
        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        public void LoseFocus()
        {
            if (style == Style.Border)
            {
                this.color = unfocusColor;
                Apply(border);

                text.color = color;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnEnter()
        {
            if (interactable)
            {
                foreach (var action in onClicked)
                {
                    action.Invoke();
                }
            }
        }
    }
}
