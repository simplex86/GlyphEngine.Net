using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 默认的启动场景
    /// </summary>
    internal class CLaunchScene : CScene
    {
        public CLaunchScene()
        {
            CCameraManager.Create("Main", 0);
            CCameraManager.Create("UI", int.MaxValue);
        }
    }
}
