namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public class CScene
    {
        /// <summary>
        /// 
        /// </summary>
        internal List<CCamera> cameras { get; } = new List<CCamera>();
        /// <summary>
        /// 
        /// </summary>
        internal List<CGameObject> gameObjects { get; } = new List<CGameObject>();

        /// <summary>
        /// 
        /// </summary>
        protected CScene()
        {
            var attrs = GetType().GetCustomAttributes(true);
            foreach (var v in attrs)
            {
                if (v is CSceneAttribute attr)
                {
                    CSceneDeserializer.Deserialize(attr.design, this);
                    break;
                }
            }
        }

        /// <summary>
        /// 进入场景
        /// </summary>
        internal protected virtual void Enter()
        {

        }

        /// <summary>
        /// 刷新场景
        /// </summary>
        /// <param name="dt"></param>
        internal protected virtual void Update(float dt)
        {

        }

        /// <summary>
        /// 离开场景
        /// </summary>
        internal protected virtual void Exit()
        {

        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="gameObject"></param>
        internal void Add(CGameObject gameObject)
        {
            if (!gameObjects.Contains(gameObject))
            {
                gameObjects.Add(gameObject);
            }
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="gameObject"></param>
        internal void Remove(CGameObject gameObject)
        {
            if (gameObjects.Contains(gameObject))
            {
                gameObjects.Remove(gameObject);
            }
        }

        /// <summary>
        /// 查找对象是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal bool Find(string name)
        {
            return Find(name, out var _);
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        internal bool Find(string name, out CGameObject gameObject)
        {
            gameObject = null;

            foreach (var o in gameObjects)
            {
                if (o.name == name)
                {
                    gameObject = o;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="name"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        internal bool Find<TObject>(string name, out TObject gameObject) where TObject : CGameObject
        {
            gameObject = null;

            foreach (var o in gameObjects)
            {
                if (o is TObject &&
                    o.name == name)
                {
                    gameObject = o as TObject;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 添加相机
        /// </summary>
        /// <param name="camera"></param>
        internal void Add(CCamera camera)
        {
            cameras.Add(camera);
        }

        /// <summary>
        /// 移除场景中已销毁的对象
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        internal void RemoveDestroyedGameObjects()
        {
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                if (gameObjects[i].destroyed)
                {
                    gameObjects.RemoveAt(i);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CSceneAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string design { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="design"></param>
        public CSceneAttribute(string design)
        {
            this.design = design;
        }
    }
}
