using Microsoft.VisualBasic.FileIO;

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
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CGameObject Load(string filepath)
        {
            return Load(filepath, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static CGameObject Load(string filepath, int x, int y)
        {
            return Load(filepath, x, y, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static CGameObject Load(string filepath, int x, int y, CGameObject parent)
        {
            var gameobject = CGameObjectDeserializer.Deserialize(filepath);
            gameobject.transform.position = new Vector2(x, y);

            if (parent == null)
            {
                var scene = CSceneManager.GetMainScene();
                scene.Add(gameobject);
            }
            else
            {
                parent.Add(gameobject);
            }

            return gameobject;
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
            // TODO: 如何处理子节点，还在思考中

            gameobject.OnDestroy();
            gameobject.destroyed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        internal protected virtual void OnDestroy()
        {
            
        }
    }
}
