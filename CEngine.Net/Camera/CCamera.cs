using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 相机
    /// </summary>
    public class CCamera
    {
        /// <summary>
        /// 渲染顺序，值越小越先被渲染
        /// </summary>
        public int order { get; }
        /// <summary>
        /// X坐标
        /// </summary>
        public int x { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public int y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; }

        /// <summary>
        /// 宽度
        /// </summary>
        internal int width { get; } = Console.BufferWidth;
        /// <summary>
        /// 高度
        /// </summary>
        internal int height { get; } = Console.BufferHeight;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public CCamera(string name)
            : this(name, 0)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public CCamera(string name, int order)
        {
            this.name = name;
            this.order = order;

            this.x = 0;
            this.y = 0;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="gameObjects"></param>
        internal void Render(List<CGameObject> gameObjects, CRenderer renderer)
        {
            foreach (var gameObject in gameObjects)
            {
                Render(gameObject, 0, 0, renderer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="renderer"></param>

        private void Render(CGameObject gameObject, int px, int py, CRenderer renderer)
        {
            px += gameObject.transform.x;
            py += gameObject.transform.y;
            // 绘制像素
            foreach (var pixel in gameObject.pixels)
            {
                if (Cull(pixel))
                {
                    var sx = px + pixel.x - x;
                    var sy = py + pixel.y - y;
                    renderer.SetPixel(sx, sy, pixel.symbol, pixel.color, pixel.backgroundColor);
                }
            }
            // 绘制子节点
            for (int i=0; i<gameObject.count; i++)
            {
                var child = gameObject.GetChild(i);
                Render(child, px, py, renderer);
            }
        }

        /// <summary>
        /// 剔除
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private bool Cull(CPixel pixel)
        {
            return true;
        }
    }
}
