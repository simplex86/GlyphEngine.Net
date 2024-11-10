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
        /// 
        /// </summary>
        ConsoleKey keycode { get; }

        /// <summary>
        /// 
        /// </summary>
        ConsoleColor focusColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        ConsoleColor unfocusColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void OnFocus();

        /// <summary>
        /// 
        /// </summary>
        void LoseFocus();

        /// <summary>
        /// 
        /// </summary>
        void OnEnter();
    }
}
