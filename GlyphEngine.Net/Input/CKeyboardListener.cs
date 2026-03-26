using System;

namespace GlyphEngine
{
    /// <summary>
    /// 键盘事件类型
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
    /// 键盘监听器
    /// </summary>
    internal class CKeyboardListener
    {
        /// <summary>
        /// 
        /// </summary>
        private EKeyboardEventType keytype = EKeyboardEventType.None;
        /// <summary>
        /// 有效的键值
        /// </summary>
        private int keycode = INVALID_KEY_CODE;
        /// <summary>
        /// 空键盘事件
        /// </summary>
        private readonly static CKeyboardEvent NONE_KEYBOARD_EVENT = new CKeyboardEvent()
        {
            type = EKeyboardEventType.None,
            keycode = INVALID_KEY_CODE,
        };
        /// <summary>
        /// 无效按键
        /// </summary>
        internal const int INVALID_KEY_CODE = -1;

        /// <summary>
        /// 轮询键盘事件
        /// </summary>
        public bool Poll(out CKeyboardEvent evt)
        {
            if (keytype == EKeyboardEventType.None)
            {
                evt = NONE_KEYBOARD_EVENT;
                return false;
            }

            evt = new CKeyboardEvent()
            {
                type = keytype,
                keycode = keycode,
            };

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        internal void Update(float dt)
        {
            if (keytype == EKeyboardEventType.Up)
            {
                keytype = EKeyboardEventType.None;
                keycode = INVALID_KEY_CODE;
            }

            if (Console.KeyAvailable)
            {
                var curkey = Console.ReadKey(true).Key;

                if (keycode == INVALID_KEY_CODE)
                {
                    OnKeyDown((int)curkey);
                    return;
                }

                if (keycode == (int)curkey)
                {
                    OnKeyHold();
                }
                else
                {
                    OnKeyUp();
                }
            }
            else if (keycode != INVALID_KEY_CODE)
            {
                OnKeyUp();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void OnKeyDown(int curkey)
        {
            keytype = EKeyboardEventType.Down;
            keycode = curkey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void OnKeyHold()
        {
            keytype = EKeyboardEventType.Hold;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void OnKeyUp()
        {
            keytype = EKeyboardEventType.Up;
        }
    }
}
