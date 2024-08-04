using System;
using System.Diagnostics;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public static class CTime
    {
        private static CTimeImp cTimeImp = null;

        internal static void SetTimeImp(CTimeImp timeImp)
        {
            cTimeImp = timeImp;
        }

        /// <summary>
        /// 单位：秒
        /// </summary>
        public static float deltatime => cTimeImp.deltatime;
    }
}
