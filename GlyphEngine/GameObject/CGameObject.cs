using System.Collections.Generic;
using System.Threading;

namespace GlyphEngine
{
    /// <summary>
    /// 游戏对象
    /// </summary>
    public partial class CGameObject : ITransformable, IContainable<CGameObject>, IClonable<CGameObject>
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
        /// 
        /// </summary>
        public long InstanceId { get; }

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
        private static long sInstanceId = 0;

        /// <summary>
        /// 
        /// </summary>
        private CGameObject()
            : this(0, 0)
        {
            
        }

        internal CGameObject(string name)
            : this(name, 0, 0)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal CGameObject(int x, int y)
            : this(null, x, y)
        {
            Transform = new CTransform(this)
            {
                LocalPosition = new Vector2(x, y)
            };
            InstanceId = Interlocked.Increment(ref sInstanceId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal CGameObject(string name, int x, int y)
        {
            Transform = new CTransform(this)
            {
                LocalPosition = new Vector2(x, y)
            };

            InstanceId = Interlocked.Increment(ref sInstanceId);

            Name = string.IsNullOrEmpty(name) ? $"gameobject_{InstanceId}" 
                                              : name;
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
        /// 
        /// </summary>
        protected virtual void OnDestroy()
        {
            
        }

        /// <summary>
        /// 克隆
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
