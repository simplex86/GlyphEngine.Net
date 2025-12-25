using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public class CScene : CGameObjectContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// 
        /// </summary>
        internal List<CCamera> Cameras { get; } = new List<CCamera>();
        /// <summary>
        /// 
        /// </summary>
        internal List<CGameObject> GameObjects { get; } = new List<CGameObject>();

        /// <summary>
        /// 
        /// </summary>
        internal CScene(string filepath)
        {
            this.FilePath = filepath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="camera"></param>
        internal void Add(CCamera camera)
        {
            Cameras.Add(camera);
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="gameobject"></param>
        internal override void Add(CGameObject gameobject)
        {
            if (!GameObjects.Contains(gameobject))
            {
                GameObjects.Add(gameobject);
            }
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="gameobject"></param>
        internal override void Remove(CGameObject gameobject)
        {
            if (GameObjects.Contains(gameobject))
            {
                GameObjects.Remove(gameobject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Destroy()
        {
            foreach (var gameobject in GameObjects)
            {
                gameobject.Destroy();
            }
            GameObjects.Clear();
        }
    }
}
