namespace SimpleX.CEngine
{
    /// <summary>
    /// 游戏对象
    /// </summary>
    public class CGameObject : IContainer
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
        public void Add(CGameObject child)
        {
            child.SetParent(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public bool Has(CGameObject child)
        {
            return children.Contains(child);
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CGameObject Get(int index)
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
            get { return Get(index); }
        }

        /// <summary>
        /// 删除子节点
        /// </summary>
        /// <param name="child"></param>
        public void Remove(CGameObject child)
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
                this.parent.Has(this))
            {
                this.parent.Remove(this);
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
        /// 销毁对象
        /// </summary>
        /// <param name="gameobject"></param>
        public static void Destroy(CGameObject gameobject)
        {
            if (gameobject == null ||
                gameobject.destroyed)
            {
                return;
            }

            // 从父结点移除
            gameobject.parent?.Remove(gameobject);
            // 处理子节点
            foreach (var child in gameobject.children)
            {
                child.OnDestroy();
                child.destroyed = true;
            }

            gameobject.OnDestroy();
            gameobject.destroyed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        internal protected virtual void OnDestroy()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual CGameObject Clone()
        {
            var clone = new CGameObject()
            {
                name = name,
                enabled = enabled,
            };
            clone.transform.position = transform.position;

            foreach (var child in children)
            {
                var clonechild = child.Clone();
                clone.Add(clonechild);
            }

            return clone;
        }
    }
}
