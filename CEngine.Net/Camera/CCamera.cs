namespace SimpleX.CEngine
{
    /// <summary>
    /// 相机
    /// </summary>
    public class CCamera
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
        /// 
        /// </summary>
        public Vector2 position { get; set; } = Vector2.zero;
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
        public CCamera(string name, uint order)
        {
            this.name = name;
            this.order = order;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        /// <param name="gameObjects"></param>
        internal void Render(List<CGameObject> gameObjects, CRenderer renderer)
        {
            foreach (var gameObject in gameObjects)
            {
                Render(gameObject, Vector2.zero, renderer);
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
        /// <param name="gameObject"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="renderer"></param>
        private void Render(CGameObject gameObject, Vector2 offset, CRenderer renderer)
        {
            if (!gameObject.enabled) return;

            var wpos = M2W(gameObject, offset);
            var vpos = W2V(wpos);

            if (gameObject is IRenderable renderable &&
                CheckLayer(renderable))
            {
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

            // 绘制子节点
            foreach (var child in gameObject.children)
            {
                Render(child, wpos, renderer);
            }
        }

        /// <summary>
        /// 模型坐标转世界坐标
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="mpos"></param>
        /// <returns></returns>
        private Vector2 M2W(CGameObject gameObject, Vector2 mpos)
        {
            return mpos + gameObject.transform.position;
        }

        /// <summary>
        /// 世界坐标转相机坐标
        /// </summary>
        /// <param name="wpos"></param>
        /// <returns></returns>
        private Vector2 W2V(Vector2 wpos)
        {
            return wpos - position + CWorld.center;
        }

        /// <summary>
        /// 剔除
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private bool Cull(int x, int y)
        {
            if (x < 0 || x >= width ||
                y < 0 || y >= height)
            {
                return false;
            }
            
            return true;
        }
    }
}
