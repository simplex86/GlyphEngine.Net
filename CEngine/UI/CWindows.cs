using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 
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
            foreach (var add in adds)
            {
                retains.Add(add);
                CWorld.Add(add.GameObject);
            }
            adds.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void PostProcess()
        {
            foreach (var remove in removes)
            {
                remove.Destroy();
                retains.Remove(remove);
            }
            removes.Clear();
        }
    }
}
