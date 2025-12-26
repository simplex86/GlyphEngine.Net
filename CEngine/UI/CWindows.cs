using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CWindows
    {
        private static List<CPanel> panels = new List<CPanel>();

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
            panels.Add(panel);
            CWorld.Add(panel.GameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        internal static void Update(float dt)
        {
            if (panels.Count == 0) return;
            panels[^1].View.Update(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        internal static void Remove(CPanel panel)
        {
            panels.Remove(panel);
        }
    }
}
