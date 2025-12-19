using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 游戏对象
    /// </summary>
    public class CGameObject : CGameObjectContainer, IClonable<CGameObject>
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string name { get; set; } = "gameobject";
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
        /// 是否已被销毁
        /// </summary>
        internal bool destroyed { get; private set; } = false;

        /// <summary>
        /// 子节点列表
        /// </summary>
        protected List<CGameObject> children { get; } = new List<CGameObject>();

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
            : this(x, y, true)
        {

        }

        internal protected CGameObject(int x, int y, bool scene)
        {
            transform = new CTransform(this);
            transform.localposition = new Vector2(x, y);

            if (scene)
            {
                CSceneManager.Add(this);
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="child"></param>
        internal override void Add(CGameObject child)
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
        internal override void Remove(CGameObject child)
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

            transform.Reposition();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Destroy()
        {
            // 从父结点移除
            parent?.Remove(this);
            // 处理子节点
            for (int i = count - 1; i >= 0; i--)
            {
                children[i].Destroy();
            }

            OnDestroy();
            destroyed = true;
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

            gameobject.Destroy();
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
            clone.transform.localposition = transform.localposition;
            clone.transform.worldposition = transform.worldposition;

            foreach (var child in children)
            {
                var clonechild = child.Clone();
                clone.Add(clonechild);
            }

            return clone;
        }
    }
}
