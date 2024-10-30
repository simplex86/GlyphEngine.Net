namespace SimpleX.CEngine
{
    /// <summary>
    /// 键盘输入
    /// </summary>
    public static class CInput
    {
        /// <summary>
        /// 任意键被按下
        /// </summary>
        public static event Action<int> OnKeyDown;
        /// <summary>
        /// 任意键保持按下状态
        /// </summary>
        public static event Action<int> OnKey;
        /// <summary>
        /// 任意键被弹起
        /// </summary>
        public static event Action<int> OnKeyUp;

        /// <summary>
        /// 有效的键值
        /// </summary>
        private static int key { get; set; } = INVALID_KEY;
        /// <summary>
        /// 无效按键
        /// </summary>
        private const int INVALID_KEY = -1;

        /// <summary>
        /// 
        /// </summary>
        internal static void Update()
        {
            if (Console.KeyAvailable)
            {
                var curkey = Console.ReadKey(true).Key;

                if (key != INVALID_KEY)
                {
                    if (key == (int)curkey)
                    {
                        OnKey?.Invoke(key);
                    }
                    else
                    {
                        OnKeyUp?.Invoke(key);
                        key = (int)curkey;
                        OnKeyDown?.Invoke(key);
                    }
                }
                else
                {
                    key = (int)curkey;
                    OnKeyDown?.Invoke(key);
                }
            }
            else if (key != INVALID_KEY)
            {
                OnKeyUp?.Invoke(key);
                key = INVALID_KEY;
            }
        }
    }
}
