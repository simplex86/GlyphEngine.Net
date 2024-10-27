using System;
using System.Collections.Generic;

namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class CUIElement : CGameObject
    {
        /// <summary>
        /// 是否可聚焦
        /// </summary>
        internal bool focusable { get; set; } = false;
        /// <summary>
        /// 是否已聚焦
        /// </summary>
        internal bool focused { get; set; } = false;

        /// <summary>
        /// 位置
        /// </summary>
        public Vector2 position { get; set; } = Vector2.zero;

        internal CUIElement()
        {

        }
    }
}
