using static System.Formats.Asn1.AsnWriter;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public class CScene : IContainer
    {
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
        /// <param name="gameobject"></param>
        public void Add(CGameObject gameobject)
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
        public void Remove(CGameObject gameobject)
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
        /// 查找对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gameobject"></param>
        /// <returns></returns>
        internal bool Find(string name, out CGameObject gameobject)
        {
            gameobject = null;

            foreach (var o in gameobjects)
            {
                if (o.name == name)
                {
                    gameobject = o;
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
        /// <param name="gameobject"></param>
        /// <returns></returns>
        internal bool Find<TObject>(string name, out TObject gameobject) where TObject : CGameObject
        {
            gameobject = null;

            foreach (var o in gameobjects)
            {
                if (o is TObject &&
                    o.name == name)
                {
                    gameobject = o as TObject;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 移除场景中已销毁的对象
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="gameobject"></param>
        /// <returns></returns>
        internal void RemoveDestroyedGameObjects()
        {
            for (int i = gameobjects.Count - 1; i >= 0; i--)
            {
                if (gameobjects[i].destroyed)
                {
                    gameobjects.RemoveAt(i);
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
