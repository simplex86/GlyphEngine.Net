namespace SimpleX.CEngine.Input
{
    /// <summary>
    /// 
    /// </summary>
    public enum EKeyboardEventType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 键按下
        /// </summary>
        Down = 1,
        /// <summary>
        /// 键（按下后）保持
        /// </summary>
        Hold = 2,
        /// <summary>
        /// 键抬起
        /// </summary>
        Up = 3,
    }

    /// <summary>
    /// 键盘事件
    /// </summary>
    public struct CKeyboardEvent
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public EKeyboardEventType type;
        /// <summary>
        /// 键值
        /// </summary>
        public int keycode;
    }

    /// <summary>
    /// 键盘输入
    /// </summary>
    public static class CKeyboard
    {
        /// <summary>
        /// 有效的键值
        /// </summary>
        private static int keycode { get; set; } = INVALID_KEY_CODE;
        /// <summary>
        /// 
        /// </summary>
        private static CKeyboardEvent NONE_KEYBOARD_EVENT = new CKeyboardEvent()
        {
            type = EKeyboardEventType.None,
            keycode = INVALID_KEY_CODE,
        };
        /// <summary>
        /// 无效按键
        /// </summary>
        internal const int INVALID_KEY_CODE = -1;

        /// <summary>
        /// 
        /// </summary>
        public static bool Poll(out CKeyboardEvent evt)
        {
            if (Console.KeyAvailable)
            {
                var curkey = Console.ReadKey(true).Key;

                if (keycode == INVALID_KEY_CODE)
                {
                    keycode = (int)curkey;
                    evt = OnKeyDown();
                }
                else
                {
                    if (keycode == (int)curkey)
                    {
                        evt = OnKeyHold();
                    }
                    else
                    {
                        evt = OnKeyUp();
                    }
                }

                return true;
            }
            
            if (keycode != INVALID_KEY_CODE)
            {
                evt = OnKeyUp();
                return true;
            }

            evt = NONE_KEYBOARD_EVENT;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static CKeyboardEvent OnKeyDown()
        {
            return new CKeyboardEvent()
            {
                type = EKeyboardEventType.Down,
                keycode = keycode,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static CKeyboardEvent OnKeyHold()
        {
            return new CKeyboardEvent()
            {
                type = EKeyboardEventType.Hold,
                keycode = keycode,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static CKeyboardEvent OnKeyUp()
        {
            var kevent = new CKeyboardEvent()
            {
                type = EKeyboardEventType.Up,
                keycode = keycode,
            };
            keycode = INVALID_KEY_CODE;

            return kevent;
        }
    }
}
