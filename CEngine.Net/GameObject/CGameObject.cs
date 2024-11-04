namespace SimpleX.CEngine
{
    /// <summary>
    /// 游戏对象
    /// </summary>
    public class CGameObject
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string name { get; set; } = "GameObject";

        /// <summary>
        /// 可见性
        /// </summary>
        public bool enabled { get; set; } = true;

        /// <summary>
        /// 位置
        /// </summary>
        public CTransform transform { get; } = null;

        /// <summary>
        /// 父节点
        /// </summary>
        public CGameObject parent { get; private set; } = null;

        /// <summary>
        /// 子节点数量
        /// </summary>
        public int count => children.Count;

        /// <summary>
        /// 子节点列表
        /// </summary>
        internal List<CGameObject> children { get; } = new List<CGameObject>();

        /// <summary>
        /// 是否已被销毁
        /// </summary>
        internal bool destroyed { get; private set; } = false;

        /// <summary>
        /// 
        /// </summary>
        internal protected CGameObject()
            : this(0, 0)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal protected CGameObject(int x, int y)
        {
            transform = new CTransform();
            transform.SetXY(x, y);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(CGameObject child)
        {
            child.SetParent(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public bool HasChild(CGameObject child)
        {
            return children.Contains(child);
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CGameObject GetChild(int index)
        {
            if (index < 0)
            {
                index = children.Count + index;
            }
            return children[index];
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CGameObject this[int index]
        {
            get { return GetChild(index); }
        }

        /// <summary>
        /// 删除子节点
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(CGameObject child)
        {
            children.Remove(child);
            child.SetParent(null);
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(CGameObject parent)
        {
            if (this.parent == parent) return;
            if (destroyed) return;
            if (parent != null && parent.destroyed) return;

            if (this.parent != null &&
                this.parent.HasChild(this))
            {
                this.parent.RemoveChild(this);
            }

            this.parent = parent;
            this.parent?.children.Add(this);

            if (this.parent == null)
            {
                var scene = CSceneManager.GetMainScene();
                scene.Add(this);
            }
            else
            {
                var scene = CSceneManager.GetMainScene();
                scene.Remove(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CGameObject CreatePrimitive()
        {
            // TODO
            return null;
        }

        /// <summary>
        /// 加载对象
        /// </summary>
        /// <typeparam name="TGameObject"></typeparam>
        /// <returns></returns>
        public static TGameObject Load<TGameObject>() where TGameObject : CGameObject, new()
        {
            var gameObject = new TGameObject();
            if (gameObject is ISkinable skinable)
            {
                skinable.LoadSkins();
            }

            var scene = CSceneManager.GetMainScene();
            scene.Add(gameObject);

            return gameObject;
        }

        /// <summary>
        /// 加载对象并设定其坐标
        /// </summary>
        /// <typeparam name="TGameObject"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static TGameObject Load<TGameObject>(int x, int y) where TGameObject : CGameObject, new()
        {
            var gameObject = new TGameObject();
            if (gameObject is ISkinable skinable)
            {
                skinable.LoadSkins();
            }
            gameObject.transform.SetXY(x, y);

            var scene = CSceneManager.GetMainScene();
            scene.Add(gameObject);

            return gameObject;
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Destroy(CGameObject gameObject)
        {
            if (gameObject == null ||
                gameObject.destroyed)
            {
                return;
            }

            // 从父结点移除
            gameObject.parent?.RemoveChild(gameObject);
            // TODO: 如何处理子节点，还在思考中

            gameObject.OnDestroy();
            gameObject.destroyed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        internal protected virtual void OnDestroy()
        {
            
        }
    }
}
