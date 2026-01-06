using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public class CScene : IContainable<CGameObject>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// 物体数量
        /// </summary>
        public int Count => GameObjects.Count;
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
        internal CScene()
        {

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
        public void Add(CGameObject gameobject)
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
        public void Remove(CGameObject gameobject)
        {
            if (GameObjects.Contains(gameobject))
            {
                GameObjects.Remove(gameobject);
            }
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
                index = GameObjects.Count + index;
            }
            return GameObjects[index];
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Update()
        {
            if (Destroyed) return;
            //
            for (int i = Cameras.Count - 1; i >= 0; i--)
            {
                if (Cameras[i].Destroyed)
                {
                    Cameras.RemoveAt(i);
                }
            }
            // 
            for (int i = GameObjects.Count - 1 ; i >= 0; i--)
            {
                if (GameObjects[i].Destroyed)
                {
                    GameObjects.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 销毁场景
        /// </summary>
        internal void Destroy()
        {
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
            // 标记
            Destroyed = true;
        }
    }
}
