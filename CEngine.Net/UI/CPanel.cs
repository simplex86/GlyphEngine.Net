using System;
using System.Collections.Generic;

namespace SimpleX.CEngine.UI
{
    public class CPanel : CGameObject
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int width { get; set; } = 50;
        /// <summary>
        /// 高度
        /// </summary>
        public int height { get; set; } = 20;
        /// <summary>
        /// 位置
        /// </summary>
        public Vector2 position { get; set; } = Vector2.zero;
    }
}
