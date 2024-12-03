namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public class CScene : CGameObjectContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public string filepath { get; }

        /// <summary>
        /// 
        /// </summary>
        internal ISceneBehaviour behaviour { get; private set; } = null;
        /// <summary>
        /// 
        /// </summary>
        internal List<CCamera> cameras { get; } = new List<CCamera>();
        /// <summary>
        /// 
        /// </summary>
        internal List<CGameObject> gameobjects { get; } = new List<CGameObject>();
        /// <summary>
        /// 
        /// </summary>
        internal bool destroyed { get; private set; } = false;

        /// <summary>
        /// 
        /// </summary>
        internal CScene(string filepath)
        {
            this.filepath = filepath;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Init()
        {
            var types = ReflectionHelper.FindAll<ISceneBehaviour, CSceneBehaviourAttribute>();
            foreach (var type in types)
            {
                var attrs = type.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    if (attr is CSceneBehaviourAttribute sba &&
                        sba.tag == filepath)
                    {
                        behaviour = Activator.CreateInstance(type) as ISceneBehaviour;
                        break;
                    }
                }
            }

            behaviour?.Enter();
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="gameobject"></param>
        internal override void Add(CGameObject gameobject)
        {
            if (gameobject is CCamera camera)
            {
                if (!cameras.Contains(camera))
                {
                    cameras.Add(camera);
                }
            }
            else
            {
                if (!gameobjects.Contains(gameobject))
                {
                    gameobjects.Add(gameobject);
                }
            }
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="gameobject"></param>
        internal override void Remove(CGameObject gameobject)
        {
            if (gameobject is CCamera camera)
            {
                if (cameras.Contains(camera))
                {
                    cameras.Remove(camera);
                }
            }
            else
            {
                if (gameobjects.Contains(gameobject))
                {
                    gameobjects.Remove(gameobject);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Destroy()
        {             
            cameras.Clear();

            foreach (var gameobject in gameobjects)
            {
                CGameObject.Destroy(gameobject);
            }
            gameobjects.Clear();

            destroyed = true;
            behaviour?.Exit();
        }
    }
}
