using System;
using System.Collections.Generic;

namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public static class CUIManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static CGameObject root = null;

        /// <summary>
        /// 
        /// </summary>
        private static List<IPanel> list = new List<IPanel>();

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<Type, IPanel> dict = new Dictionary<Type, IPanel>();

        /// <summary>
        /// 打开UI
        /// </summary>
        /// <typeparam name="TPanel"></typeparam>
        public static TPanel Open<TPanel>() where TPanel : IPanel, new()
        {
            var panel = new TPanel();
            list.Add(panel);
            dict.Add(typeof(TPanel), panel);
            // 添加到根节点
            root.AddChild(panel.gameObject);

            return panel;
        }

        internal static void Update(float dt)
        {
            if (list.Count > 0)
            {
                var top = list[^1];
                top.Update(dt);
            }
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <typeparam name="TPanel"></typeparam>
        public static void Close<TPanel>() where TPanel : IPanel
        {
            var type = typeof(TPanel);
            if (dict.TryGetValue(typeof(TPanel), out var panel))
            {
                dict.Remove(type);
                list.Remove(panel);

                CGameObject.Destroy(panel.gameObject);
            }
        }

        /// <summary>
        /// UI是否已经打开
        /// </summary>
        /// <typeparam name="TPanel"></typeparam>
        /// <returns></returns>
        public static bool IsOpened<TPanel>() where TPanel : IPanel
        {
            return dict.ContainsKey(typeof(TPanel));
        }

        /// <summary>
        /// 设置根节点
        /// </summary>
        /// <param name="gameObject"></param>
        internal static void SetRoot(CGameObject gameObject)
        {
            root = gameObject;
        }
    }
}
