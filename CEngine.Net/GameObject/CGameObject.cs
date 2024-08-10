using System.Reflection;

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
        public string Name { get; set; } = "GameObject";

        /// <summary>
        /// 可见性
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 位置
        /// </summary>
        public CTransform Transform { get; }

        /// <summary>
        /// 位置 - X坐标
        /// </summary>
        public int X
        {
            get { return Transform.X; }
            set { Transform.X = value; }
        }

        /// <summary>
        /// 位置 - Y坐标
        /// </summary>
        public int Y
        {
            get { return Transform.Y; }
            set { Transform.Y = value; }
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public CGameObject Parent { get; private set; }

        /// <summary>
        /// 子节点数量
        /// </summary>
        public int Count => children.Count;

        /// <summary>
        /// 子节点列表
        /// </summary>
        internal List<CGameObject> children { get; } = new List<CGameObject>();

        /// <summary>
        /// 像素列表
        /// </summary>
        internal List<CPixel> pixels { get; } = new List<CPixel>();

        /// <summary>
        /// 是否已被销毁
        /// </summary>
        internal bool destroyed { get; private set; } = false;

        /// <summary>
        /// 皮肤
        /// </summary>
        private Dictionary<string, CSkin> skins = new Dictionary<string, CSkin>();

        /// <summary>
        /// 
        /// </summary>
        protected CGameObject()
            : this(0, 0)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected CGameObject(int x, int y)
        {
            Transform = new CTransform()
            {
                X = x,
                Y = y,
            };
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
            if (this.destroyed) return;
            if (parent.destroyed) return;
            if (Parent == parent) return;

            Parent?.RemoveChild(this);
            Parent = parent;
            Parent?.children.Add(this);

            if (Parent == null)
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
        /// 添加像素
        /// </summary>
        /// <param name="pixel"></param>
        protected void AddPixel(CPixel pixel)
        {
            pixels.Add(pixel);
        }

        /// <summary>
        /// 加载皮肤
        /// </summary>
        protected void LoadSkins()
        {
            var types = ReflectionHelper.FindAll<CSkin, CSkinOfAttribute>();
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<CSkinOfAttribute>();
                if (attr != null && attr.Is(GetType()))
                {
                    var skin = Activator.CreateInstance(type) as CSkin;
                    AddSkin(skin, attr.applied);
                }
            }
        }

        /// <summary>
        /// 添加皮肤
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skin"></param>
        protected void AddSkin(CSkin skin, bool apply = false)
        {
            if (skins.TryGetValue(skin.Name, out var _))
            {
                skins[skin.Name] = skin;
            }
            else
            {
                skins.Add(skin.Name, skin);
            }

            if (apply)
            {
                skin.Apply(this);
            }
        }

        /// <summary>
        /// 应用指定名字的皮肤
        /// </summary>
        /// <param name="skinName"></param>
        public void ApplySkin(string skinName)
        {
            if (skins.TryGetValue(skinName, out var skin))
            {
                skin.Apply(this);
            }
        }

        /// <summary>
        /// 移除皮肤
        /// </summary>
        /// <param name="skinName"></param>
        protected void RemoveSkin(string skinName)
        {
            skins.Remove(skinName);
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
        public static TGameObject Load<TGameObject>() where TGameObject : CGameObject
        {
            var gameObject = Activator.CreateInstance<TGameObject>();
            if (gameObject != null)
            {
                gameObject.LoadSkins();

                var scene = CSceneManager.GetMainScene();
                scene.Add(gameObject);
            }

            return gameObject;
        }

        /// <summary>
        /// 加载对象并设定其坐标
        /// </summary>
        /// <typeparam name="TGameObject"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static TGameObject Load<TGameObject>(int x, int y) where TGameObject : CGameObject
        {
            var gameObject = Activator.CreateInstance<TGameObject>();
            if (gameObject != null)
            {
                gameObject.LoadSkins();
                gameObject.Transform.SetXY(x, y);

                var scene = CSceneManager.GetMainScene();
                scene.Add(gameObject);
            }

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

            gameObject.destroyed = true;

            var parent = gameObject.Parent;
            if (parent != null)
            {
                parent.RemoveChild(gameObject);
                gameObject.SetParent(null);
            }
        }
    }
}
