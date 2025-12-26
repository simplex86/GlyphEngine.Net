using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 游戏对象
    /// </summary>
    public class CGameObject : ITransformable, IContainable<CGameObject>, IClonable<CGameObject>
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; } = "gameobject";
        /// <summary>
        /// 可见性
        /// </summary>
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// 位置
        /// </summary>
        public CTransform Transform { get; } = null;
        /// <summary>
        /// 父节点
        /// </summary>
        public CGameObject Parent { get; private set; } = null;
        /// <summary>
        /// 子节点数量
        /// </summary>
        public int Count => Children.Count;

        /// <summary>
        /// 是否已被销毁
        /// </summary>
        internal bool Destroyed { get; private set; } = false;

        /// <summary>
        /// 子节点列表
        /// </summary>
        protected List<CGameObject> Children { get; } = new List<CGameObject>();

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
            Transform = new CTransform(this);
            Transform.LocalPosition = new Vector2(x, y);

            if (scene)
            {
                CWorld.Add(this);
            }
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
        /// 删除子节点
        /// </summary>
        /// <param name="child"></param>
        public void Remove(CGameObject child)
        {
            Children.Remove(child);
            child.SetParent(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public bool Has(CGameObject child)
        {
            return Children.Contains(child);
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
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CGameObject GetChild(int index)
        {
            if (index < 0)
            {
                index = Children.Count + index;
            }
            return Children[index];
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(CGameObject parent)
        {
            if (Parent == parent) return;
            if (Destroyed) return;
            if (parent != null && parent.Destroyed) return;

            if (Parent != null &&
                Parent.Has(this))
            {
                Parent.Remove(this);
            }

            Parent = parent;
            Parent?.Children.Add(this);

            Transform.Reposition();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Destroy()
        {
            // 从父结点移除
            Parent?.Remove(this);
            // 处理子节点
            for (int i = Count - 1; i >= 0; i--)
            {
                Children[i].Destroy();
            }

            OnDestroy();
            Destroyed = true;
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="gameobject"></param>
        public static void Destroy(CGameObject gameobject)
        {
            if (gameobject == null ||
                gameobject.Destroyed)
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
                Name = Name,
                Enabled = Enabled,
            };
            clone.Transform.LocalPosition = Transform.LocalPosition;
            clone.Transform.WorldPosition = Transform.WorldPosition;

            foreach (var child in Children)
            {
                var clonechild = child.Clone();
                clone.Add(clonechild);
            }

            return clone;
        }
    }
}
