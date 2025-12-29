using System;
using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 按钮
    /// </summary>
    public class CButton : CWidget, IInteractable
    {
        /// <summary>
        /// 是否可交互
        /// </summary>
        public bool Interactabled { get; set; } = true;
        /// <summary>
        /// 响应的按键
        /// </summary>
        public ConsoleKey Keycode { get; } = ConsoleKey.Enter;
        /// <summary>
        /// 获得焦点时的颜色
        /// </summary>
        public ConsoleColor FocusColor { get; set; } = ConsoleColor.Red;
        /// <summary>
        /// 失去焦点时的颜色
        /// </summary>
        public ConsoleColor UnfocusColor { get; set; } = ConsoleColor.White;

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
        /// <param name="text"></param>
        /// <param name="localposition"></param>
        /// <param name="unfocusColor"></param>
        /// <param name="focusColor"></param>
        /// <param name="style"></param>
        internal CButton(string text, Vector2 localposition, ConsoleKey keycode, ConsoleColor unfocusColor, ConsoleColor focusColor, EBorderStyle style = EBorderStyle.ThinBorder)
            : base()
        {
            this.Interactabled = true;
            this.Transform.LocalPosition = localposition;
            this.Keycode = keycode;
            this.UnfocusColor = unfocusColor;
            this.FocusColor = focusColor;

            this.text = new CText(text, Vector2.Zero);
            Add(this.text);

            this.Width  = this.text.Width  + 4;
            this.Height = this.text.Height + 2;

            Apply(style);
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
            Color = FocusColor;
            text.Color = Color;
        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        public void LoseFocus()
        {
            Color = UnfocusColor;
            text.Color = Color;
        }

        /// <summary>
        /// 响应点击
        /// </summary>
        public void OnEnter()
        {
            if (!Interactabled) return;

            foreach (var action in onClicked)
            {
                action.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroy()
        {
            onClicked.Clear();
            base.OnDestroy();
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
    }
}
