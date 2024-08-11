namespace SimpleX.CEngine
{
    /// <summary>
    /// 键盘输入
    /// </summary>
    public static class CInput
    {
        /// <summary>
        /// 键盘按下
        /// </summary>
        public static Action<int> OnKeyDown;
        /// <summary>
        /// 键盘按下后保持
        /// </summary>
        public static Action<int> OnKey;
        /// <summary>
        /// 键盘抬起
        /// </summary>
        public static Action<int> OnKeyUp;

        private static CInputImp cInputImp = null;

        internal static void SetTimeImp(CInputImp inputImp)
        {
            cInputImp = inputImp;

            cInputImp.OnKeyDown += (key) => OnKeyDown?.Invoke(key);
            cInputImp.OnKey     += (key) => OnKey?.Invoke(key);
            cInputImp.OnKeyUp   += (key) => OnKeyUp?.Invoke(key);
        }
    }
}
