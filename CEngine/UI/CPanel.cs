using System.Collections.Generic;

namespace CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class CPanel
    {
        /// <summary>
        /// 
        /// </summary>
        public CGameObject gameobject => view;

        /// <summary>
        /// 
        /// </summary>
        internal CPanelView view { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetComponent<T>(string name) where T : CWidget
        {
            return view.GetComponent<T>(name);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal static class CPanelManager
    {
        private static List<CPanel> panels = new List<CPanel>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        internal static void Add(CPanel panel)
        {
            panels.Add(panel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        internal static void Update(float dt)
        {
            if (panels.Count == 0) return;
            panels[^1].view.Update(dt);
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
