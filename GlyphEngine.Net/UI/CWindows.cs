using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 窗口管理器
    /// </summary>
    internal static class CWindows
    {
        private static CGameObject root;

        private static List<CPanel> adds = new List<CPanel>();
        private static List<CPanel> retains = new List<CPanel>();

        /// <summary>
        /// 
        /// </summary>
        public static void Init()
        {
            CWorld.Add(new CCamera("ui_camera", uint.MaxValue)
            {
                Mask = (ulong)ERenderMask.UI,
            });

            root = new CGameObject("ui_root", 0, 0);
            CWorld.Add(root);
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
        private static void PrevProcess()
        {
            foreach (var panel in adds)
            {
                retains.Add(panel);
                panel.SetParent(root);
                panel.Start();
            }
            adds.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void PostProcess()
        {
            for (int i = retains.Count - 1; i >= 0; i--)
            {
                var panel = retains[i];
                if (panel.Destroyed)
                {
                    retains.RemoveAt(i);
                    panel.DestroyImmediately();
                }
            }
        }
    }
}
