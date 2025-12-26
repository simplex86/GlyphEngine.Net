using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 相机
    /// </summary>
    public class CCamera : ITransformable
    {
        /// <summary>
        /// 
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
        public uint Order { get; } = 0;
        /// <summary>
        /// 渲染遮罩
        /// </summary>
        public ulong Mask { get; set; } = (ulong)ERenderMask.Everything;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; internal set; } = CScreen.Width;
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; internal set; } = CScreen.Height;
        /// <summary>
        /// 可见性
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 是否已被销毁
        /// </summary>
        internal bool Destroyed { get; private set; } = false;

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
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="renderobjects"></param>
        internal void Render(List<CGameObject> gameobjects, CRenderer renderer)
        {
            foreach (var gameobject in gameobjects)
            {
                Render(gameobject, renderer);
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
            if (gameobject.Enabled &&
                gameobject is IRenderable renderable &&
                renderable.Enabled &&
                CheckLayer(renderable))
            {
                var wpos = M2W(gameobject);
                var vpos = W2V(wpos);
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
                // 绘制子节点
                for (int i = 0; i < gameobject.Count; i++)
                {
                    Render(gameobject[i], renderer);
                }
            }
        }

        /// <summary>
        /// 模型坐标转世界坐标
        /// </summary>
        /// <param name="gameobject"></param>
        /// <param name="mpos"></param>
        /// <returns></returns>
        private Vector2 M2W(CGameObject gameobject)
        {
            return gameobject.Transform.WorldPosition;
        }

        /// <summary>
        /// 世界坐标转相机坐标
        /// </summary>
        /// <param name="wpos"></param>
        /// <returns></returns>
        private Vector2 W2V(Vector2 wpos)
        {
            return wpos - Transform.WorldPosition + CScreen.Center;
        }

        /// <summary>
        /// 剔除
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private bool Cull(int x, int y)
        {
            var x1 = (CScreen.Width - Width) / 2;
            var x2 = x1 + Width;
            var y1 = (CScreen.Height - Height) / 2;
            var y2 = y1 + Height;

            if (x < x1 || x > x2 ||
                y < y1 || y > y2)
            {
                return false;
            }
            
            return true;
        }
    }
}
