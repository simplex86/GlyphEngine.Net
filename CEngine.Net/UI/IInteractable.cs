namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// 是否可交互
        /// </summary>
        bool interactable { get; set; }

        /// <summary>
        /// 响应交互的按键
        /// </summary>
        ConsoleKey keycode { get; }

        /// <summary>
        /// 获得焦点时的颜色
        /// </summary>
        ConsoleColor focusColor { get; set; }

        /// <summary>
        /// 失去焦点时的颜色
        /// </summary>
        ConsoleColor unfocusColor { get; set; }

        /// <summary>
        /// 获得焦点
        /// </summary>
        void OnFocus();

        /// <summary>
        /// 失去焦点
        /// </summary>
        void LoseFocus();

        /// <summary>
        /// 响应点击
        /// </summary>
        void OnEnter();
    }
}
