namespace CEngine
{
    /// <summary>
    /// 相机
    /// </summary>
    public class CCamera : CGameObject
    {
        /// <summary>
        /// 渲染顺序
        /// 值越小越先被渲染
        /// </summary>
        public uint order { get; } = 0;
        /// <summary>
        /// 渲染遮罩
        /// </summary>
        public ulong mask { get; set; } = (ulong)ERenderMask.Everything;
        /// <summary>
        /// 宽度
        /// </summary>
        public int width { get; internal set; } = Console.BufferWidth;
        /// <summary>
        /// 高度
        /// </summary>
        public int height { get; internal set; } = Console.BufferHeight;

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
            this.name = name;
            this.order = order;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="renderobjects"></param>
        internal void Render(List<CGameObject> gameobjects, CRenderer renderer)
        {
            if (destroyed) return;

            foreach (var gameobject in gameobjects)
            {
                Render(gameobject, renderer);
            }
        }

        /// <summary>
        /// 检查渲染层
        /// </summary>
        /// <param name="renderable"></param>
        /// <returns></returns>
        private bool CheckLayer(IRenderable renderable)
        {
            return (renderable.layer & mask) == renderable.layer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="renderer"></param>
        private void Render(CGameObject gameobject, CRenderer renderer)
        {
            if (gameobject.enabled &&
                gameobject is IRenderable renderable &&
                renderable.enabled &&
                CheckLayer(renderable))
            {
                var wpos = M2W(gameobject);
                var vpos = W2V(wpos);
                // 绘制像素
                renderable.Foreach(pixel =>
                {
                    var x = vpos.x + pixel.x;
                    var y = vpos.y + pixel.y;

                    if (Cull(x, y))
                    {
                        renderer.SetPixel(x, y, pixel.c, pixel.color, pixel.backgroundColor);
                    }
                });
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
            return gameobject.transform.worldposition;
        }

        /// <summary>
        /// 世界坐标转相机坐标
        /// </summary>
        /// <param name="wpos"></param>
        /// <returns></returns>
        private Vector2 W2V(Vector2 wpos)
        {
            return wpos - transform.worldposition + CWorld.center;
        }

        /// <summary>
        /// 剔除
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private bool Cull(int x, int y)
        {
            var x1 = (CWorld.width - width) / 2;
            var x2 = x1 + width;
            var y1 = (CWorld.height - height) / 2;
            var y2 = y1 + height;

            if (x < x1 || x > x2 ||
                y < y1 || y > y2)
            {
                return false;
            }
            
            return true;
        }
    }
}
