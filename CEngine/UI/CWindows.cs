using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 窗口管理器
    /// </summary>
    internal static class CWindows
    {
        private static List<CPanel> adds = new List<CPanel>();
        private static List<CPanel> retains = new List<CPanel>();
        private static List<CPanel> removes = new List<CPanel>();

        /// <summary>
        /// 
        /// </summary>
        public static void Init()
        {
            CWorld.Add(new CCamera("ui_camera", uint.MaxValue)
            {
                Mask = (ulong)ERenderMask.UI,
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        internal static void Add(CPanel panel)
        {
            adds.Add(panel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        internal static void Update(float dt)
        {
            PrevProcess();
            {
                foreach (var panel in retains)
                {
                    panel.Update(dt);
                }
            }
            PostProcess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        internal static void Remove(CPanel panel)
        {
            removes.Add(panel);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void PrevProcess()
        {
            foreach (var panel in adds)
            {
                retains.Add(panel);
                CWorld.Add(panel.GameObject);

                panel.Open();
            }
            adds.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void PostProcess()
        {
            foreach (var panel in removes)
            {
                panel.Destroy();
                retains.Remove(panel);
            }
            removes.Clear();
        }
    }
}
