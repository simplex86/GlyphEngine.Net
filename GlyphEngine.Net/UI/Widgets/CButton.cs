using System;
using System.Collections.Generic;

namespace GlyphEngine
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
        public CColor FocusColor { get; set; } = CColor.Red;
        /// <summary>
        /// 失去焦点时的颜色
        /// </summary>
        public CColor UnfocusColor { get; set; } = CColor.White;

        /// <summary>
        /// 
        /// </summary>
        private EBorderStyle style = EBorderStyle.Borderless;
        /// <summary>
        /// 点击事件列表
        /// </summary>
        private List<Action> onClicked = new List<Action>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="localposition"></param>
        /// <param name="unfocusColor"></param>
        /// <param name="focusColor"></param>
        /// <param name="style"></param>
        internal CButton(CVector2 localposition, int width, int height, ConsoleKey keycode, in CColor unfocusColor, in CColor focusColor, EBorderStyle style = EBorderStyle.Thin)
            : base(localposition)
        {
            this.Width = width;
            this.Height = height;
            this.Interactabled = true;
            this.Keycode = keycode;
            this.UnfocusColor = unfocusColor;
            this.FocusColor = focusColor;

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
        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        public void LoseFocus()
        {
            Color = UnfocusColor;
        }

        /// <summary>
        /// 响应点击
        /// </summary>
        public void OnEnter()
        {
            if (Interactabled)
            {
                foreach (var action in onClicked)
                {
                    action.Invoke();
                }
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

            this.style = style;
        }
    }
}
