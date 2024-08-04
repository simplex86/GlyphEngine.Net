using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 键盘输入
    /// </summary>
    public static class CInput
    {
        public static Action<int> OnKeyDown;
        public static Action<int> OnKey;
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
