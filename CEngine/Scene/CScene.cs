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
        public string Name { get; internal set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Destroyed { get; private set; } = false;

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
        /// <param name="camera"></param>
        internal void Add(CCamera camera)
        {
            if (Destroyed) return;
            Cameras.Add(camera);
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="gameobject"></param>
        internal override void Add(CGameObject gameobject)
        {
            if (Destroyed) return;

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
            if (Destroyed) return;

            if (GameObjects.Contains(gameobject))
            {
                GameObjects.Remove(gameobject);
            }
        }

        /// <summary>
        /// 销毁场景
        /// </summary>
        internal void Destroy()
        {
            Destroyed = true;
            // 销毁相机
            foreach (var camera in Cameras)
            {
                camera.Destroy();
            }
            Cameras.Clear();
            // 销毁物件
            foreach (var gameobject in GameObjects)
            {
                gameobject.Destroy();
            }
            GameObjects.Clear();
        }
    }
}
