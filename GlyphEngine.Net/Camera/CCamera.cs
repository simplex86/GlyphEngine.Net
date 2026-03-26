using System;
using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 相机
    /// </summary>
    public class CCamera : ITransformable
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; } = "camera";
        /// <summary>
        /// 
        /// </summary>
        public CTransform Transform { get; } = new CTransform();
        /// <summary>
        /// 渲染顺序
        /// 值越小越先被渲染
        /// </summary>
        internal uint Order { get; } = 0;
        /// <summary>
        /// 渲染遮罩
        /// </summary>
        internal ulong Mask { get; set; } = (ulong)ERenderMask.Everything;
        /// <summary>
        /// 宽度
        /// </summary>
        internal int Width
        {
            set
            {
                width = Math.Min(value, CScreen.Width);
                CalculateRect();
            }
            get {  return width; }
        }
        /// <summary>
        /// 高度
        /// </summary>
        internal int Height
        {
            set
            {
                height = Math.Min(value, CScreen.Height);
                CalculateRect();
            }
            get { return height; }
        }
        /// <summary>
        /// 可见性
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 是否已被销毁
        /// </summary>
        internal bool Destroyed { get; private set; } = false;

        private int width  = CScreen.Width;
        private int height = CScreen.Height;

        private int minx = 0;
        private int miny = 0;
        private int maxx = 0;
        private int maxy = 0;

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
        public CCamera(string name, uint order)
        {
            this.Name = name;
            this.Order = order;

            CalculateRect();
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="renderobjects"></param>
        internal void Render(List<CGameObject> gameobjects, CRenderer renderer)
        {
            foreach (var gameobject in gameobjects)
            {
                if (gameobject.Enabled)
                {
                    Render(gameobject, renderer);
                }
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        internal void Destroy()
        {
            Destroyed = true;
        }

        /// <summary>
        /// 检查渲染层
        /// </summary>
        /// <param name="renderable"></param>
        /// <returns></returns>
        private bool CheckLayer(IRenderable renderable)
        {
            return (renderable.Layer & Mask) == renderable.Layer;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="gameobject"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="renderer"></param>
        private void Render(CGameObject gameobject, CRenderer renderer)
        {
            if (gameobject is IRenderable renderable &&
                renderable.Enabled &&
                CheckLayer(renderable))
            {
                var wpos = M2W(gameobject);
                var cpos = W2C(wpos);
                var vpos = C2V(cpos);
                // 绘制像素
                renderable.Foreach(pixel =>
                {
                    var x = vpos.X + pixel.X;
                    var y = vpos.Y + pixel.Y;

                    if (Cull(x, y))
                    {
                        renderer.SetPixel(x, y, pixel.Glyph, pixel.Color, pixel.BackgroundColor);
                    }
                });
            }

            // 绘制子节点
            for (int i = 0; i < gameobject.Count; i++)
            {
                var child = gameobject[i];
                if (child.Enabled) Render(gameobject[i], renderer);
            }
        }

        /// <summary>
        /// 模型坐标转世界坐标
        /// </summary>
        /// <param name="gameobject"></param>
        /// <param name="mpos"></param>
        /// <returns></returns>
        private CVector2 M2W(CGameObject gameobject)
        {
            return gameobject.Transform.WorldPosition;
        }

        /// <summary>
        /// 世界坐标转相机坐标
        /// </summary>
        /// <param name="wpos"></param>
        /// <returns></returns>
        private CVector2 W2C(CVector2 wpos)
        {
            return wpos - Transform.WorldPosition;
        }

        /// <summary>
        /// 相机坐标转屏幕坐标
        /// </summary>
        /// <param name="cpos"></param>
        /// <returns></returns>
        private CVector2 C2V(CVector2 cpos)
        {
            return cpos + CScreen.Center;
        }

        /// <summary>
        /// 剔除
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private bool Cull(int x, int y)
        {
            var wpos = Transform.WorldPosition;

            if (x < minx + wpos.X || x > maxx + wpos.X ||
                y < miny + wpos.Y || y > maxy + wpos.Y)
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// 计算相机范围
        /// </summary>
        private void CalculateRect()
        {
            minx = Math.Max(0, (CScreen.Width  - Width)  / 2);
            miny = Math.Max(0, (CScreen.Height - Height) / 2);
            maxx = Math.Min(CScreen.Width,  minx + Width);
            maxy = Math.Min(CScreen.Height, miny + Height);
        }
    }
}
