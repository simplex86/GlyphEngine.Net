using System;
using System.Collections.Generic;

namespace SimpleX.CEngine.UI
{
    public class CPanel<T> where T : CPanelView
    {
        /// <summary>
        /// 
        /// </summary>
        public T view { get; }

        protected CPanel()
        {
            view = Activator.CreateInstance<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        protected void AddElement(CUIElement child)
        {
            view?.AddChild(child);
        }

        private void Init()
        {

        }
    }
}
