using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 键盘输入
    /// </summary>
    public static class CInput
    {
        /// <summary>
        /// 
        /// </summary>
        internal static void Update()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
            }
        }
    }
}
